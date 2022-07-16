namespace ChromaWrapper.Tests.Internal
{
    internal static class MetaTestsCommon
    {
        public static string GetSolutionDirectory()
        {
            const string SolutionFilename = "ChromaWrapper.sln";

            var fi = new FileInfo(typeof(MetaTestsCommon).Assembly.Location);

            for (var di = fi.Directory; di != null; di = di.Parent)
            {
                if (di.GetFiles(SolutionFilename, SearchOption.TopDirectoryOnly).Length != 0)
                {
                    return di.FullName;
                }
            }

            throw new InvalidOperationException("Solution directory could not be found.");
        }
    }
}
