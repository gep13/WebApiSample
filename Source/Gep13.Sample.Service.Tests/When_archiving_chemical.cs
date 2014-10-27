using Gep13.Sample.Data.Infrastructure;
using Gep13.Sample.Data.Repositories;
using Gep13.Sample.Model;
using NSubstitute;
using NUnit.Framework;

namespace Gep13.Sample.Service.Test 
{
    
    [TestFixture]
    public class When_archiving_chemical 
    {
        private IChemicalRepository fakeChemicalRepository;
        private IUnitOfWork fakeUnitOfWork;
        private ChemicalService chemicalService;
        
        [SetUp]
        public void Setup() 
        {
            fakeChemicalRepository = Substitute.For<IChemicalRepository>();
            fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            chemicalService = new ChemicalService(fakeChemicalRepository, fakeUnitOfWork);            
        }

        [Test]
        public void Should_return_false_if_unable_to_find_chemical() 
        {
            fakeChemicalRepository.GetById(1).Returns(x => null);

            var actual = chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void Should_return_true_if_archives_chemical() 
        {
            var entity = new Chemical 
            {
                Id = 1
            };

            fakeChemicalRepository.GetById(1).Returns(x => entity);

            var actual = chemicalService.ArchiveChemical(1);

            Assert.That(actual, Is.True);
            Assert.That(entity.IsArchived, Is.True);
        }
    }
}