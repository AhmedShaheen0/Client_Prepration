using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using TestPrepation.Data;
using TestPrepation.Services;
using TestPrepation.Data.Consts;
using TestPrepation.Data.Models;
using TestPrepation.Data.ViewModels;

public class ClientsController : Controller
{
    private readonly IClientRepo _clientRepo;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _webHostEnvironment;

    private List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };
    private int _maxAllowedSize = 2097152;

    public ClientsController(IMapper mapper, IWebHostEnvironment webHostEnvironment, IClientRepo clientRepo)
    {
        _mapper = mapper;
        _webHostEnvironment = webHostEnvironment;
        _clientRepo = clientRepo;
    }



    // GET: ClientsController
    public ActionResult Index()
    {
      
        var repo = _clientRepo.GetAllClients();

        var viewModel = _mapper.Map<IEnumerable<ClientViewModel>>(repo);
        
        
        return View("Index", viewModel);
        

    }

    [HttpGet]
    // GET: ClientsController/Create
    public ActionResult Create()
    {

        return PartialView("_Form", PopulateViewModel());
    }

    // POST: ClientsController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(ClientViewModel model)
    {
        if (!ModelState.IsValid)
            return View("_Form", PopulateViewModel(model));
        var repo = _mapper.Map<Client>(model);


        if (model.Image is not null)
        {
            var extension = Path.GetExtension(model.Image.FileName);

            if (!_allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(model.Image), Errors.NotAllowedExtension);
                return View("_Form", PopulateViewModel(model));
            }

            if (model.Image.Length > _maxAllowedSize)
            {
                ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                return View("_Form", PopulateViewModel(model));
            }

            var imageName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/images/books", imageName);

            using var stream = System.IO.File.Create(path);
            model.Image.CopyTo(stream);

            repo.ImagePath = imageName;
        }
        
        _clientRepo.AddClient(repo);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]

    // GET: ClientsController/Edit/5
    public ActionResult Edit(int id)
    {
        var repo = _clientRepo.GetClientById(id);

        if (repo is null) return NotFound();

        // mapping GuardViewModel to Guard
        var ViewModel = _mapper.Map<ClientViewModel>(repo);


        return PartialView("_Form", ViewModel);
    }

    // POST: ClientsController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(ClientViewModel model)
    {
        if (!ModelState.IsValid)
            return View("_Form", PopulateViewModel(model));

        var repo = _clientRepo.GetClientById(model.ClientId);
        if (repo is null)
            return NotFound();

        if (model.Image is not null)
        {
            if (!string.IsNullOrEmpty(repo.ImagePath))
            {
                var oldImagePath = Path.Combine($"{_webHostEnvironment.WebRootPath}/images", repo.ImagePath);

                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            var extension = Path.GetExtension(model.Image.FileName);

            if (!_allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(nameof(model.Image), Errors.NotAllowedExtension);
                return View("_Form", PopulateViewModel(model));
            }

            if (model.Image.Length > _maxAllowedSize)
            {
                ModelState.AddModelError(nameof(model.Image), Errors.MaxSize);
                return View("_Form", PopulateViewModel(model));
            }

            var imageName = $"{Guid.NewGuid()}{extension}";

            var path = Path.Combine($"{_webHostEnvironment.WebRootPath}/images", imageName);

            using var stream = System.IO.File.Create(path);
            model.Image.CopyTo(stream);

            model.ImagePath = imageName;
        }

        else if (model.Image is null && !string.IsNullOrEmpty(repo.ImagePath))
            model.ImagePath = repo.ImagePath;

        repo = _mapper.Map(model, repo);
        
        _clientRepo.UpdateClient(repo);

        return RedirectToAction(nameof(Index));

    }
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var client = _clientRepo.GetClientById(id);
        if (client != null)
        {
            _clientRepo.DeleteClient(id);
        }

        return RedirectToAction(nameof(Index));

    }
    private ClientViewModel PopulateViewModel(ClientViewModel? model = null)
    {
        ClientViewModel viewModel = model is null ? new ClientViewModel() : model;

        var status = _clientRepo.GetAllMaritalStatuses();

        viewModel.SelectStatus = _mapper.Map<IEnumerable<SelectListItem>>(status);

        return viewModel;
    }
}
