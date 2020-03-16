using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FullStack.Domain.Interfaces.Business;

namespace FullStack.Api.Infrastructure
{
    public class BaseController : ControllerBase
    {
        #region Protected Properties

        protected ILogger Logger { get; }
        protected IMapper Mapper { get; }
        protected IUnitOfWork UnitOfWork { get; }

        #endregion

        #region Constructors

        public BaseController(ILogger logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        #endregion
    }
}
