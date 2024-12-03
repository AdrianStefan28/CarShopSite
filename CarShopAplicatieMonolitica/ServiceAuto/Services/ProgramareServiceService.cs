using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Services
{
    public class ProgramareServiceService : IProgramareServiceService
    {
        private readonly IProgramareServiceRepository programareServiceRepository;

        public ProgramareServiceService(IProgramareServiceRepository programareServiceRepository)
        {
            this.programareServiceRepository = programareServiceRepository;
        }

        public IEnumerable<ProgramareService> GetProgramareServices()
        {
            return programareServiceRepository.GetAll();
        }
        public ProgramareService GetProgramareService(int id)
        {
            return programareServiceRepository.Get(id); 
        }
        public ProgramareService AddProgramareService(ProgramareService programareService)
        {
            return programareServiceRepository.Add(programareService);
        }
        public ProgramareService UpdateProgramareService(ProgramareService programareService)
        {
            return programareServiceRepository.Update(programareService);
        }
        public void DeleteProgramareService(int id)
        {
            programareServiceRepository.Remove(id);
        }

    }
}
