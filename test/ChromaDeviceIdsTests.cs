using System;
using System.Collections.Generic;
using System.Reflection;
using ChromaWrapper.Data;
using Xunit;

namespace ChromaWrapper.Tests
{
    public class ChromaDeviceIdsTests
    {
        [Fact]
        public void AllDeclaredFieldsReturnDistinctDeviceIds()
        {
            var allFields = typeof(ChromaDeviceIds).GetFields(BindingFlags.Public | BindingFlags.Static);

            var allIds = new HashSet<Guid>();

            foreach (var fi in allFields)
            {
                var id = Assert.IsType<Guid>(fi.GetValue(null));
                allIds.Add(id);
            }

            Assert.Equal(allFields.Length, allIds.Count);
        }
    }
}
