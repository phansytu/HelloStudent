namespace src {
    public interface ISubjectService{
        public List<Subject> GetAllSubjects();
        public Subject FindByMaMon(string maMon);
        public Subject FindByTenMon(string tenMon);
        public bool IsSubjectExists(string tenMon);
        public bool AddSubject(string tenMon, int soTinChi);
        public void UpdateSubject(Subject subject);
        public void DeleteSubject(string maMon);

        
    }
}