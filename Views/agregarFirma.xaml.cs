using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Maui.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Tarea2._2FirmaDigital.Utilities;
using Tarea2._2FirmaDigital.Controllers;

namespace Tarea2._2FirmaDigital.Views;

public partial class agregarFirma : ContentPage
{
    private bool _firmaExiste = false;
    Controllers.firmaController _firmaController;

    public agregarFirma()
	{
		InitializeComponent();
        _firmaController = new Controllers.firmaController();
	}

    private void btnBorrar_Clicked(object sender, EventArgs e)
    {
        drawingViewFirma.Clear();
        _firmaExiste = false;
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        string descripcion = entryDescripcion.Text;
        string nombre = entryNombre.Text;
        


        if (_firmaExiste == true)
        {
            if(string.IsNullOrEmpty(descripcion)) 
            {
                await DisplayAlert("Aviso", "Por favor ingrese una descripci�n!", "OK");
                return;
            }
            else if(string.IsNullOrEmpty(nombre))
            {
                await DisplayAlert("Aviso", "Por favor ingrese su nombre!", "OK");
                return;
            }
        }
        else
        {
            await DisplayAlert("Aviso", "Por favor ingrese su firma!", "OK");
            return;
        }

        string base64Img = await base64Image.ConvertToBase64Async(drawingViewFirma);

        var firma = new Models.firmaDigital
        {
            Descripcion = descripcion,
            NombreFirma = nombre,
            FirmaDigital = base64Img
        };

        SaveImageAsync(base64Img);

        try
        {
            if (_firmaController != null)
            {
                if (await _firmaController.storeFirma(firma) > 0)
                {
                    await DisplayAlert("Aviso", "Registro Ingresado con Exito!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Ocurrio un Error", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrio un Error: {ex.Message}", "OK");
        }

    }

    private async void SaveImageAsync(string base64Img)
    {
        ImageSavingService imageSavingService = ImageSavingService.Instance;
        if (imageSavingService == null)
        {
            // Handle the case where the service couldn't be resolved
            await DisplayAlert("Error", "Failed to resolve IImageSavingService", "OK");
            return;
        }

        string randomFileName = Path.GetRandomFileName();
        string randomImageName = $"firma_{randomFileName.Replace(".", "")}.png";
        string savedFilePath = await imageSavingService.SaveImageAsync(base64Img, randomImageName, "FirmasDigitales");

        if (savedFilePath != null)
        {
            // Image saved successfully, handle accordingly
            await DisplayAlert("Success", $"Image saved to {savedFilePath}", "OK");
        }
        else
        {
            // Image saving failed or permission denied, handle accordingly
            await DisplayAlert("Error", "Failed to save image", "OK");
        }
    }

    private void drawingViewFirma_DrawingLineStarted(object sender, CommunityToolkit.Maui.Core.DrawingLineStartedEventArgs e)
    {
        _firmaExiste = true;
    }
}