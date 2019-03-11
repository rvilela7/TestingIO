namespace ParserCSVTest
{
    public interface IFileCSV
    {
        void ReadFile2DB(long iterations = 10, int progressLength = 20);
        void Write2File(long iterations = 10, int progressLength = 20);
    }
}