using Microsoft.Maui.Controls;
using SQLite;
using System.Collections.ObjectModel;
using Tarea2._2FirmaDigital.Controllers;
using Tarea2._2FirmaDigital.Models;

namespace Tarea2._2FirmaDigital.Views;

public partial class verFirmas : ContentPage
{
	private firmaController controller;
    private List<Models.firmaDigital> firmas;
    public verFirmas()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        controller = new firmaController();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        firmas = await controller.getListFirmas();

        collectionViewFirmas.ItemsSource = firmas;
    }

}