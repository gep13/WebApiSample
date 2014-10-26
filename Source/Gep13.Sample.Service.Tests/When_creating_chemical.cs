// --------------------------------------------------------------------------------------------------------------------
// <copyright file="When_creating_chemical.cs" company="Gary Ewan Park">
//   Copyright (c) Gary Ewan Park, 2014, All rights reserved.
// </copyright>
// <summary>
//   Defines the When_creating_chemical type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Reflection.Emit;

namespace Gep13.Sample.Service.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    using AutoMapper;

    using Gep13.Sample.Data.Infrastructure;
    using Gep13.Sample.Data.Repositories;
    using Gep13.Sample.Model;

    using NSubstitute;

    using NUnit.Framework;

    [TestFixture]
    public class When_creating_chemical
    {
        private IChemicalRepository fakeChemicalRepository;
        private IUnitOfWork fakeUnitOfWork;
        private ChemicalService chemicalService;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {           
            Mapper.CreateMap<ChemicalDto, Chemical>();
            Mapper.CreateMap<Chemical, ChemicalDto>();
        }

        [SetUp]
        public void SetUp()
        {
            this.fakeChemicalRepository = Substitute.For<IChemicalRepository>();
            this.fakeUnitOfWork = Substitute.For<IUnitOfWork>();
            this.chemicalService = new ChemicalService(this.fakeChemicalRepository, this.fakeUnitOfWork);
        }

        [Test]
        public void Should_create_chemical()
        {
            this.fakeChemicalRepository.GetMany(Arg.Any<Expression<Func<Chemical, bool>>>()).ReturnsForAnyArgs(x => new List<Chemical>());
            
            this.fakeChemicalRepository.Add(Arg.Do<Chemical>(x => x.Id = 1)).Returns(new Chemical { Id = 1 });
            
            var chemical = this.chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical.Id, Is.EqualTo(1));
            this.fakeUnitOfWork.Received().SaveChanges();
        }

        [Test]
        public void Should_return_null_if_chemical_with_same_name_already_exists() 
        {
            this.fakeChemicalRepository.GetMany(Arg.Any<Expression<Func<Chemical, bool>>>()).ReturnsForAnyArgs(x => new List<Chemical>{ new Chemical{Id=1}});

            var chemical = this.chemicalService.AddChemical("First", 110.99);

            Assert.That(chemical, Is.Null);
        }
    }
}
