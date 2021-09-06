using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using ChromaWrapper.Internal;
using ChromaWrapper.Mouse;
using ChromaWrapper.Sdk;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class EffectTests
    {
        private static readonly IReadOnlySet<Type> _effectInterfaces = new HashSet<Type>(GetAllEffectInterfaces());

        [Theory]
        [MemberData(nameof(GetAllEffectTypes))]
        public void EffectsIsEitherStaticOrCustom(Type type)
        {
            Assert.Matches(@"^(Static|Custom)\w+Effect", type.Name);
        }

        [Theory]
        [MemberData(nameof(GetAllEffectTypes))]
        public void EffectImplementsASingleEffectInterface(Type type)
        {
            var ti = Assert.Single(type.GetInterfaces().Intersect(_effectInterfaces));

            string tSuffix = ti.Name[1..];
            Assert.True(type.Name.EndsWith(tSuffix, StringComparison.Ordinal)
                || type.Name.EndsWith($"{tSuffix}2", StringComparison.Ordinal));

            object o = Activator.CreateInstance(type)!;

            var piEffectType = ti.GetProperty("EffectType")!;
            object v = piEffectType.GetValue(o)!;
            Assert.NotEqual(0, (int)v);
        }

        [Theory]
        [MemberData(nameof(GetStaticEffectTypes))]
        public void StaticEffectIsBlittableAndHasSizeOfCOLORREF(Type type)
        {
            object o = Activator.CreateInstance(type)!;
            int sz = Marshal.SizeOf(o);

            int expected = ChromaColorTests.SizeOfCOLORREF;

            // Exception: StaticMouseEffect includes a private, deprecated, LEDId field.
            if (type == typeof(StaticMouseEffect))
            {
                expected += Marshal.SizeOf<int>();
            }

            Assert.Equal(expected, sz);
        }

        [Theory]
        [MemberData(nameof(GetStaticEffectTypes))]
        public void StaticEffectImplementsIStaticEffect(Type type)
        {
            var o = (IStaticEffect)Activator.CreateInstance(type)!;

            var c = ChromaColor.FromRgb(5, 10, 15);
            o.Color = c;
            Assert.Equal(c, o.Color);
        }

        [Theory]
        [MemberData(nameof(GetCustomEffectTypes))]
        public void CustomEffectImplementsIColorBuffer(Type type)
        {
            Assert.Contains(typeof(IColorBuffer), type.GetInterfaces());
        }

        [Theory]
        [MemberData(nameof(GetCustomEffectTypes))]
        public void CustomEffectImplementsLedOrKeyEffectInterfaces(Type type)
        {
            var ledKeyEffectInterfaces = new[] { typeof(ILedArrayEffect), typeof(ILedGridEffect), typeof(IKeyGridEffect) };

            Assert.NotEmpty(type.GetInterfaces().Intersect(ledKeyEffectInterfaces));

            object o = Activator.CreateInstance(type)!;

            if (o is ILedArrayEffect la)
            {
                Assert.NotEqual(0, la.Color.Count);
            }

            if (o is ILedGridEffect lg)
            {
                Assert.NotEqual(0, lg.Color.Rows);
                Assert.NotEqual(0, lg.Color.Columns);
            }

            if (o is IKeyGridEffect kg)
            {
                Assert.NotEqual(0, kg.Key.Rows);
                Assert.NotEqual(0, kg.Key.Columns);
            }
        }

        internal static IEnumerable<object[]> GetStaticEffectTypes()
        {
            return from oa in GetAllEffectTypes()
                   let t = (Type)oa[0]
                   where t.Name.StartsWith("Static", StringComparison.Ordinal)
                   select oa;
        }

        internal static IEnumerable<object[]> GetCustomEffectTypes()
        {
            return from oa in GetAllEffectTypes()
                   let t = (Type)oa[0]
                   where t.Name.StartsWith("Custom", StringComparison.Ordinal)
                   select oa;
        }

        private static IEnumerable<object[]> GetAllEffectTypes()
        {
            var rx = new Regex(@"^\w+Effect2?$");

            return from t in typeof(ChromaSdk).Assembly.ExportedTypes
                   where t.IsClass && rx.IsMatch(t.Name)
                   select new object[] { t };
        }

        private static IEnumerable<Type> GetAllEffectInterfaces()
        {
            var rx = new Regex(@"^I(ChromaLink|Headset|Keyboard|Keypad|Mouse|Mousepad)Effect$");

            return from t in typeof(ChromaSdk).Assembly.ExportedTypes
                   where t.IsInterface && rx.IsMatch(t.Name)
                   select t;
        }
    }
}
