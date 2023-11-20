using System.ComponentModel.Design;
using System.Linq.Expressions;

List<Product> products = new List<Product>()
{
    new Product()
    {
    Name = "Cloak of Invisibility",
    Price = 19.99M,
    Available = false,
    ProductTypeId = 1,
    DateStocked = new DateTime(2023, 9, 23)
    },
    new Product()
    {
    Name = "Boots of Fire Resistance",
    Price = 29.99M,
    Available = true,
    ProductTypeId = 1,
    DateStocked = new DateTime(2023, 10, 11)
    },
    new Product()
    {
    Name = "Wizards Hat of Wisdom",
    Price = 21.99M,
    Available = true,
    ProductTypeId = 1,
    DateStocked = new DateTime(2023, 10, 31)
    },
    new Product()
    {
    Name = "Healing Potion",
    Price = 99.99M,
    Available = false,
    ProductTypeId = 2,
    DateStocked = new DateTime(2023, 11, 1)
    },
    new Product()
    {
    Name = "Invincibility Potion",
    Price = 199.99M,
    Available = true,
    ProductTypeId = 2,
    DateStocked = new DateTime(2023, 10, 1)
    },
    new Product()
    {
    Name = "Levitation Potion",
    Price = 39.99M,
    Available = true,
    ProductTypeId = 2,
    DateStocked = new DateTime(2023, 11, 11)
    },
    new Product()
    {
    Name = "Mirror of Truth",
    Price = 9.99M,
    Available = true,
    ProductTypeId = 3,
    DateStocked = new DateTime(2023, 9, 10)
    },
    new Product()
    {
    Name = "Crystal Ball of Knowledge",
    Price = 3.99M,
    Available = true,
    ProductTypeId = 3,
    DateStocked = new DateTime(2023, 10, 4)
    },
    new Product()
    {
    Name = "Flying Broomstick",
    Price = 59.99M,
    Available = true,
    ProductTypeId = 3,
    DateStocked = new DateTime(2023, 9, 19)
    },
    new Product()
    {
    Name = "Elder Wand",
    Price = 299.99M,
    Available = true,
    ProductTypeId = 4,
    DateStocked = new DateTime(2023, 10, 2)
    },
    new Product()
    {
    Name = "Phoenix Feather Wand",
    Price = 149.99M,
    Available = true,
    ProductTypeId = 4,
    DateStocked = new DateTime(2023, 9, 1)
    },
    new Product()
    {
    Name = "Crystal Wand of Elemental Mastery",
    Price = 8.99M,
    Available = false,
    ProductTypeId = 4,
    DateStocked = new DateTime(2023, 10, 28)
    },

};

List<ProductType> productTypes = new List<ProductType>()
{
    new ProductType()
    {
    Name = "Apparel",
    Id = 1
    },
    new ProductType()
    {
    Name = "Potions",
    Id = 2
    },
    new ProductType()
    {
    Name = "Enchanted Objects",
    Id = 3
    },
    new ProductType()
    {
    Name = "Wands",
    Id = 4
    }

};

string greeting = @"Welcome to Reductio & Absurdum
Your one-stop shop for all things magic.";

Console.WriteLine(greeting);

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"
Select an option:
    0. Exit
    1. View All Products
    2. Add a Product To Inventory
    3. Delete a Product From Inventory
    4. Update a Products Details
    5. Search by Product Type
    6. View Available Products"
    );


    if (int.TryParse(Console.ReadLine(), out int userChoice))
    {
        Console.Clear();

        switch (userChoice)
        {
            case 0:
                Console.WriteLine("See Ya!");
                Environment.Exit(0);
                break;

            case 1:
                ListProducts();
                break;

            case 2:
                AddProductToInventory();
                break;

            case 3:
                DeleteProductFromInventory();
                break;

            case 4:
                UpdateProductProperties();
                break;

            case 5:
                SearchByType();
                break;

            case 6:
                ViewAvailableProducts();
                break;
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Enter a number between 0 and 7.");
    }

}

void ListProducts()
{
    Console.WriteLine("All Products In Inventory:");

    for (int i = 0; i < products.Count; i++)
    {
        Console.WriteLine(@$"{i + 1}. {products[i].Name} {(products[i].Available ? $"is available for ${products[i].Price} and has been on shelves for {products[i].DaysOnShelf} days" : "is not available")}.");

    }
}


void AddProductToInventory()
{
    Product newProduct = new Product();

    // Product Name
    Console.WriteLine("Product Name:");
    newProduct.Name = Console.ReadLine().Trim();

    //Product Price
    Console.WriteLine("Product Price:");
    if (decimal.TryParse(Console.ReadLine().Trim(), out decimal Price))
    {
        newProduct.Price = Price;
    }

    //Product Available
    newProduct.Available = true;

    //Product Type ID
    Console.WriteLine(@"Enter a Product Type ID #
    1. Apparel
    2. Potions
    3. Enchanted Objects
    4. Wands");

    if (int.TryParse(Console.ReadLine(), out int ProductTypeId))
    {
        newProduct.ProductTypeId = ProductTypeId;
    }

    //Product Stock Date
    newProduct.DateStocked = DateTime.Now;

    products.Add(newProduct);
}

void DeleteProductFromInventory()
{
    ListProducts();

    Product gettingDeleted = null;

    while (gettingDeleted == null)
    {
        Console.WriteLine("Enter the product number you wish to delete:");

        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            gettingDeleted = products[response - 1];

            products.RemoveAt(response - 1);

            Console.WriteLine($"{gettingDeleted.Name} was successfully deleted.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Type integers only.");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Choose an existing product.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}

void UpdateProductProperties()
{
    ListProducts();

    Product chosenProduct = null;
    bool invalidChoice = true;

    while (invalidChoice)
    {
        Console.WriteLine("Enter the number of the product you wish to modify:");

        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenProduct = products[response - 1];

            if (!chosenProduct.Available)
            {
                Console.WriteLine("Product is not available. Choose another.");
            }
            //Edit Product Name
            else
            {
                Console.WriteLine($"Enter the new name for {chosenProduct.Name}:");
                chosenProduct.Name = Console.ReadLine();

                Console.WriteLine($"Enter the new price for {chosenProduct.Name}:");
                chosenProduct.Price = decimal.Parse(Console.ReadLine());

                Console.WriteLine(@"Enter a Product Type ID #
                1. Apparel
                2. Potions
                3. Enchanted Objects
                4. Wands");
                chosenProduct.ProductTypeId = int.Parse(Console.ReadLine());
                
                
                Console.WriteLine($"This product now has a new name of {chosenProduct.Name} and price of {chosenProduct.Price}.");
                invalidChoice = false;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

void SearchByType()
{
    Console.WriteLine(@"Enter the Product Type # for the products you wish to view
    1. Apparel
    2. Potions
    3. Enchanted Objects
    4. Wands ");

    if (int.TryParse(Console.ReadLine().Trim(), out int selectedType))
    {
        List<Product> selectedProducts = products.Where(p => p.ProductTypeId == selectedType).ToList();

        selectedProducts.Select((p) => $"* {p.Name}")
        .ToList().
        ForEach(Console.WriteLine);
    }
    else
    {
        {
            Console.WriteLine("Incorrect format. Please enter a number between 1 and 4.");
        }
    }

}

//Example of using Where and Select to print available products to the console
//Select is similar to map and Where is similar to filter
void ViewAvailableProducts()
{
    Console.WriteLine("Only the available products:");

    List<Product> unsoldProducts = products.Where(p => p.Available).ToList();

    unsoldProducts
         .Select((p) => $"* {p.Name}")
         .ToList()
         .ForEach(Console.WriteLine);

}

