using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionLogController : ControllerBase
    {
        private readonly ITransactionLogService _transactionLogService;
        private readonly IChangeLogService _changeLogService;

        public TransactionLogController(ITransactionLogService transactionLogService, IChangeLogService changeLogService)
        {
            _transactionLogService = transactionLogService;
            _changeLogService = changeLogService;
        }

        [HttpGet]
        public ActionResult Get(Guid entityId, string className)
        {
            var transactionLogs = _transactionLogService.GetTransactionLogs(entityId, className);
            var viewModel = new List<TransactionLogViewModel>();
            viewModel = transactionLogs.Adapt<List<TransactionLogViewModel>>();
            return Ok(viewModel);
        }
    }
}