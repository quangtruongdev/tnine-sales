using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using tnine.Application.Shared.IColorService;
using tnine.Web.Host.Models;

namespace tnine.Web.Host.Controllers
{
    public class HomeController : Controller
    {
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public HomeController(IColorService colorService, IMapper mapper)
        {
            _colorService = colorService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            var colors = await _colorService.GetAll();

            var colorViewModels = _mapper.Map<IEnumerable<ColorViewModel>>(colors);

            return View(colorViewModels);
        }
    }
}
