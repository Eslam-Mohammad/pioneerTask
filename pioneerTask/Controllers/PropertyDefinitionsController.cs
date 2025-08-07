using Microsoft.AspNetCore.Mvc;
using pioneerTask.Enums;
using pioneerTask.Interfaces.Services;
using pioneerTask.ViewModels;
using System.Threading.Tasks;

namespace pioneerTask.Controllers
{
    public class PropertyDefinitionsController : Controller
    {
        private readonly IPropertyDefinitionService _propertyDefinitionService;

        public PropertyDefinitionsController(IPropertyDefinitionService propertyDefinitionService)
        {
            _propertyDefinitionService = propertyDefinitionService;
        }

        public async Task<IActionResult> Index()
        {
            var properties = await _propertyDefinitionService.GetAllDefinitionsAsync(); 
            return View(properties);
        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await _propertyDefinitionService.GetDefinitionByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        public IActionResult Create()
        {
            var model = new PropertyDefinitionViewModel
            {
                Options = new List<string>() 
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyDefinitionViewModel definitionVm)
        {
            if (ModelState.IsValid)
            {
                await _propertyDefinitionService.CreateDefinitionAsync(definitionVm);
                return RedirectToAction(nameof(Index));
            }
            return View(definitionVm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var property = await _propertyDefinitionService.GetDefinitionByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PropertyDefinitionViewModel definitionVm)
        {
            if (id != definitionVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _propertyDefinitionService.UpdateDefinitionAsync(definitionVm);
                return RedirectToAction(nameof(Index));
            }

           
            if (definitionVm.Type == PropertyType.Dropdown && definitionVm.Options == null)
            {
                var existing = await _propertyDefinitionService.GetDefinitionByIdAsync(id);
                definitionVm.Options = existing?.Options ?? new List<string>();
            }

            return View(definitionVm);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var property = await _propertyDefinitionService.GetDefinitionByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _propertyDefinitionService.DeleteDefinitionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}