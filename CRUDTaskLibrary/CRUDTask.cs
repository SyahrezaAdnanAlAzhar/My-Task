namespace CRUDTaskLibrary
{
    public static class CRUDTask
    {
        public enum JenisTugas { Video, Laporan, Project, Desain, Proposal, SlidePresentasi, Observasi, Quiz, ForumDiskusi};

        public static string getKodeJenisTugas(JenisTugas jenisTugas)
        {
            string[] KodeJenisTugas = { "F-1V", "F-2L", "F-3P", "F-4D", "F-5P", "F-6S", "NF-1O", "NF-2Q", "NF-3F"};
            return KodeJenisTugas[(int) jenisTugas];
        }


    }
}
