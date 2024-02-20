using GameController;

namespace StudentTestPicker.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class ClassStudents : ContentPage
{

    public string ItemId
    {
        set { LoadStudens(value); }
    }
    public ClassStudents()
	{
		InitializeComponent();
	}

    public void LoadStudens(string classNumber)
    {
        BindingContext = new Models.AllStudents(classNumber);
    }

    private void BoughtButton_Chagned(object sender, EventArgs e)
    {
        Models.Product p = new Models.Product(ProductFilename, ProductName, ProductCategory, ProductUnit, ProductAmmount);
        p.setBought(ProductBought);
        ProductBought = p.isBought;
    }
}