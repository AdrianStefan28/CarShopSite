using ServiceAuto.Models;

namespace ServiceAuto.Services.Interfaces
{
    public interface IProgramareServiceService
    {
        IEnumerable<ProgramareService> GetProgramareServices();
        ProgramareService GetProgramareService(int id);
        ProgramareService AddProgramareService(ProgramareService programareService);
        ProgramareService UpdateProgramareService(ProgramareService programareService);
        void DeleteProgramareService(int id);
    }
}
