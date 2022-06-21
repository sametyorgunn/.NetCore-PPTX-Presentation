using Aspose.Slides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using upload_pptx.Models;
using upload_pptx.Helpers;
using Microsoft.AspNetCore.Hosting;

namespace upload_pptx.Controllers
{
    public class HomeController : Controller
    {
		slide s = new slide();
		public List<Helpers.slide> slides;
        public IHostingEnvironment _env;
		public HomeController(IHostingEnvironment env)
		{
			_env = env;
		}

		[HttpGet]
		public IActionResult UploadPresentation()
		{
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> UploadPresentation(slide obj)
		{
			Context context = new Context();
			if (ModelState.IsValid)
			{
				string wwwRootPath = _env.WebRootPath;
				string filename = Path.GetFileNameWithoutExtension(obj.file.FileName);
				string extension = Path.GetExtension(obj.file.FileName);
				obj.Yol = filename = filename + extension;//yol == path
				string path = Path.Combine(wwwRootPath + "/Presentations/", filename);  // + DateTime.Now.ToString("yymmssfff") 
				using (var filestream = new FileStream(path, FileMode.Create))
				{
					await obj.file.CopyToAsync(filestream);
				}
				context.slides.Add(obj);
				context.SaveChanges();
				await context.SaveChangesAsync();
				//return View();
				return RedirectToAction("PresentationList", "Home");
			}
			return View(obj);
		}
		public IActionResult PresentationList()
        {
			Context c = new Context();
			var results = c.slides.ToList();
			return View(results);
        }


		[HttpGet]
		public IActionResult Index(string Yol,string FileName)    //string fileName
		{
			
			slides = new List<Helpers.slide>();
			if (FileName == null) //a to FileName
			{

				// Display default PowerPoint file on page load
				slides = RenderPresentationAsImage(Yol);  //"6.pptx"

			}
			else
			{
				
				slides = RenderPresentationAsImage(FileName);
			}

			return View(slides);
		}
		




		public List<Helpers.slide> RenderPresentationAsImage(string FileName) //string FileName
		{
			var webRoot = _env.WebRootPath;
			// Load the PowerPoint presentation
			Presentation presentation = new Presentation(Path.Combine(webRoot, "Presentations", FileName));  //FileName
			var slides = new List<Helpers.slide>();

			string imagePath = "";
			// Save and view presentation slides
			for (int j = 0; j < presentation.Slides.Count; j++)
			{
				ISlide sld = presentation.Slides[j];
				Bitmap bmp = sld.GetThumbnail(1f, 1f);
				imagePath = Path.Combine(webRoot, "Slides", string.Format("slide_{0}.png", j));
				bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
				// Add to the list
				slides.Add(new Helpers.slide { SlideNumber = j, Yol = string.Format("slide_{0}.png", j) }); //yol == path
			}

			return slides;
		}

		

			[HttpGet]
		public IActionResult UploadPage()
        {
			return View();
        }
		[HttpPost]
		public IActionResult UploadPage(slide slide)
		{
			Context context = new Context();
			context.slides.Add(slide);
			context.SaveChanges();
			return View();
		}
	}
}
