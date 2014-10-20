// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_updating_chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_updating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Gep13.Sample.Service.Test
{
    using System.Reflection;

    using AutoMapper;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_updating_chemical
    {
        private IChemicalRepository fakeChemicalRepository;
        private IUnitOfWork fakeUnitOfWork;
        private ChemicalService chemicalService;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Mapper.CreateMap<ChemicalDTO, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDTO>();
        }

        [SetUp]
        public void SetUp()
        {
            this.fakeChemicalRepository = Substitute.For<IChemicalRepository>();
            this.fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            this.chemicalService = new ChemicalService(this.fakeChemicalRepository, this.fakeUnitOfWork);
        }

        [Test]
        public void Should_update()
        {
            var toUpdate = new Service.ChemicalDTO
            {
                Id = 1,
                Balance = 110.99,
                Name = "First"
            };

            var actual = this.chemicalService.UpdateChemical(toUpdate);

            this.fakeChemicalRepository.Received().Update(Arg.Any<Chemical>());
            this.fakeUnitOfWork.Received().SaveChanges();
        }
    }
}