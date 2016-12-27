using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Moq;
using Domain.Abstract;
using Domain.Entities;
using Domain.Concrete;

namespace WebUI.Infrastructure
{
    // реализация пользовательской фабрики контроллеров,
    // наследуясь от фабрики используемой по умолчанию
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel _ninjectKernel;
        public NinjectControllerFactory()
        {
            // создание контейнера
            _ninjectKernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            // конфигурирование контейнера
            Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
                mock.Setup(m => m.Products).Returns(new List<Product> {
                                                    new Product { Name = "Football", Price = 25 },
                                                    new Product { Name = "Surf board", Price = 179 },
                                                    new Product { Name = "Running shoes", Price = 95 }
                                                    }.AsQueryable());
            //_ninjectKernel.Bind<IProductsRepository>().ToConstant(mock.Object);
            _ninjectKernel.Bind<IProductsRepository>().To<EFProductRepository>();

        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            // получение объекта контроллера из контейнера
            // используя его тип
            return controllerType == null ? null : (IController)_ninjectKernel.Get(controllerType);

        }


    }
}