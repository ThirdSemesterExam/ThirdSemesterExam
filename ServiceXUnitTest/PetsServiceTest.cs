using Application.Interfaces;
using Domain;
using Moq;
using Application;
namespace XUnitTest
{
    public class PetsServiceTest
    {
        private List<Pets> fakeRepo = new List<Pets>();
        private Mock<IPetsRepository> petsRepoMock = new Mock<IPetsRepository>();

        public PetsServiceTest()
        {
            petsRepoMock.Setup(x => x.GetAll()).Returns(fakeRepo);
            petsRepoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns<int>(id => fakeRepo.FirstOrDefault(x => x.Id == id));
            petsRepoMock.Setup(x => x.Add(It.IsAny<Pets>())).Callback<Pets>(s => fakeRepo.Add(s));
            petsRepoMock.Setup(x => x.Update(It.IsAny<Pets>())).Callback<Pets>(s =>
            {
                var index = fakeRepo.IndexOf(s);
                if (index != -1)
                    fakeRepo[index] = s;
            });
            petsRepoMock.Setup(x => x.Delete(It.IsAny<Pets>())).Callback<Pets>(s => fakeRepo.Remove(s));
        }
    }
}