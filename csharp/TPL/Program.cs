namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string demo = "Sample11";

            switch(demo)
            {
                case "Sample1": Sample01.Demo();
                    break;

                case "Sample2": Sample02.Demo();
                    break;

                case "Sample3":
                    Sample03.Demo();
                    break;

                case "Sample4":
                    Sample04.Demo();
                    break;

                case "Sample5":
                    Sample05.Demo();
                    break;

                case "Sample6":
                    Sample06.Demo();
                    break;

                case "Sample7":
                    Sample07.Demo();
                    break;

                case "Sample8":
                    Sample08.Demo();
                    break;

                case "Sample9":
                    Sample09.Demo();
                    break;

                case "Sample10":
                    Sample10.Demo();
                    break;

                case "Sample11":
                    Sample11.Demo();
                    break;

                default:                    
                    break;
            }
        }
    }
}
