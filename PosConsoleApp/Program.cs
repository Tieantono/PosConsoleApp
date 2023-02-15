using PosConsoleApp.Models;

var hasInputExit = false;

var masterProducts = new List<MasterProduct>
{
    new MasterProduct
    {
        ProductId = 1,
        Name = "Apple",
        Price = 10_000,
        Quantity = 10
    }
};

while (hasInputExit == false)
{
    var input = string.Empty;
    Console.Clear();

    Console.WriteLine("1. Manage Products");
    Console.WriteLine("2. Purchase Products");
    Console.WriteLine("3. View Transactions");
    Console.WriteLine("4. Exit");
    Console.WriteLine("Please choose menu: ");

    input = Console.ReadLine();

    if (int.TryParse(input, out int parsedInput))
    {
        switch (parsedInput)
        {
            case 1:
                ViewManageProductsMenu();
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                hasInputExit = true;
                break;
            // Handle the default or when input is not 1, 2, 3, or 4.
            default:
                break;
        }
    }
}

void ViewManageProductsMenu()
{
    var manageProductMenuInput = string.Empty;
    Console.Clear();

    Console.WriteLine("1. Add New Product");
    Console.WriteLine("2. Update Stock");
    Console.WriteLine("3. Delete Product");
    Console.WriteLine("4. Back");
    Console.WriteLine("Please choose menu: ");

    ViewMasterProductList();

    manageProductMenuInput = Console.ReadLine();

    if (int.TryParse(manageProductMenuInput, out int parsedInput))
    {
        switch (parsedInput)
        {
            case 1:
                AddNewProduct();
                break;
            case 2:
                UpdateProductStock();
                break;
            case 3:
                DeleteProduct();
                break;
            case 4:
                hasInputExit = true;
                break;
            default:
                break;
        }
    }
}

void AddNewProduct()
{
    var newProduct = new MasterProduct();

    // Validate Product Name.
    var isValidProductName = false;
    Console.WriteLine("Enter the Product Name (3 - 32):");
    while (!isValidProductName)
    {
        var newProductNameInput = Console.ReadLine();

        // Will validate whether the string is null or empty ("").
        if (string.IsNullOrEmpty(newProductNameInput))
        {
            Console.WriteLine("Product Name must not empty.");
            // Avoid using break or continue in loop if possible, to avoid unreadable loop codes.
            continue;
        }

        if (newProductNameInput.Length < 3 || newProductNameInput.Length > 32)
        {
            Console.WriteLine("Product Name length must be between 3 and 32 characters.");
            continue;
        }

        newProduct.Name = newProductNameInput;
        isValidProductName = true;
    }

    // Validate Product Price.
    var isValidProductPrice = false;
    Console.WriteLine("Enter the Product Price (500 - 1.000.000):");
    while (!isValidProductPrice)
    {
        var newProductPriceInput = Console.ReadLine();

        // Will validate whether the string is null or empty ("").
        if (string.IsNullOrEmpty(newProductPriceInput))
        {
            Console.WriteLine("Product Price must not empty.");
            continue;
        }

        if (!decimal.TryParse(newProductPriceInput, out var newProductPrice))
        {
            Console.WriteLine("Product Price must be numeric.");
            continue;
        }

        if (newProductPrice < 500 || newProductPrice > 1_000_000)
        {
            Console.WriteLine("Product Price must be between 500 and 1.000.000.");
            continue;
        }

        newProduct.Price = newProductPrice;
        isValidProductPrice = true;
    }

    // Validate Product Quantity.
    var isValidProductQuantity = false;
    Console.WriteLine("Enter the Product Quantity (1 - 100):");
    while (!isValidProductQuantity)
    {
        var newProductQuantityInput = Console.ReadLine();

        // Will validate whether the string is null or empty ("").
        if (string.IsNullOrEmpty(newProductQuantityInput))
        {
            Console.WriteLine("Product Quantity must not empty.");
            continue;
        }

        if (!int.TryParse(newProductQuantityInput, out var newProductQuantity))
        {
            Console.WriteLine("Product Quantity must be numeric.");
            continue;
        }

        if (newProductQuantity < 1 || newProductQuantity > 100)
        {
            Console.WriteLine("Product Quantity must be between 1 and 100.");
            continue;
        }

        newProduct.Quantity = newProductQuantity;
        isValidProductQuantity = true;
    }

    // Find the latest Product ID.
    var latestProductId = masterProducts
        // Use LINQ to order by descending to obtain the latest ProductId.
        .OrderByDescending(Q => Q.ProductId)
        // Get the ProductId object only.
        .Select(Q => Q.ProductId)
        // Get the first item in the list.
        .FirstOrDefault();

    newProduct.ProductId = latestProductId + 1;
    masterProducts.Add(newProduct);
}

void UpdateProductStock()
{
    var updatedProduct = new MasterProduct();

    // Validate Product ID.
    var isValidProductId = false;
    Console.WriteLine("Enter the Product ID:");
    while (!isValidProductId)
    {
        var productIdInput = Console.ReadLine();

        // Will validate whether the string is null or empty ("").
        if (string.IsNullOrEmpty(productIdInput))
        {
            Console.WriteLine("Product ID must not empty.");
            continue;
        }

        if (!int.TryParse(productIdInput, out var productId))
        {
            Console.WriteLine("Product ID must be numeric.");
            continue;
        }

        // ! will tell compiler that the object won't be null.
        // DON'T ABUSE THIS!
        var existingProduct = masterProducts!
            .Where(Q => Q.ProductId == productId)
            .FirstOrDefault();

        if (existingProduct == null)
        {
            Console.WriteLine("Product ID does not exist.");
            continue;
        }

        // Object by reference.
        updatedProduct = existingProduct;
        isValidProductId = true;
    }

    // Validate Product Quantity.
    // You can make this validation into a method for reusability.
    var isValidProductQuantity = false;
    Console.WriteLine("Enter the Product Quantity (1 - 100):");
    while (!isValidProductQuantity)
    {
        var newProductQuantityInput = Console.ReadLine();

        // Will validate whether the string is null or empty ("").
        if (string.IsNullOrEmpty(newProductQuantityInput))
        {
            Console.WriteLine("Product Quantity must not empty.");
            continue;
        }

        if (!int.TryParse(newProductQuantityInput, out var newProductQuantity))
        {
            Console.WriteLine("Product Quantity must be numeric.");
            continue;
        }

        if (newProductQuantity < 1 || newProductQuantity > 100)
        {
            Console.WriteLine("Product Quantity must be between 1 and 100.");
            continue;
        }

        // The Product's quantity in masterProducts will updated automatically,
        // due to object by reference behavior.
        // Visualization: masterProducts[0] -> existingProduct -> updatedProduct
        // masterProducts[0].Quantity will automatically updated since updatedProduct is just a reference to masterProducts[0].
        updatedProduct.Quantity = newProductQuantity;
        isValidProductQuantity = true;
    }
}

void DeleteProduct()
{
    var deletedProduct = new MasterProduct();

    // Validate Product ID.
    // You can make this validation into a method for reusability.
    var isValidProductId = false;
    Console.WriteLine("Enter the Product ID:");
    while (!isValidProductId)
    {
        var productIdInput = Console.ReadLine();

        // Will validate whether the string is null or empty ("").
        if (string.IsNullOrEmpty(productIdInput))
        {
            Console.WriteLine("Product ID must not empty.");
            continue;
        }

        if (!int.TryParse(productIdInput, out var productId))
        {
            Console.WriteLine("Product ID must be numeric.");
            continue;
        }

        // ! will tell compiler that the object won't be null.
        // DON'T ABUSE THIS!
        var existingProduct = masterProducts!
            .Where(Q => Q.ProductId == productId)
            .FirstOrDefault();

        if (existingProduct == null)
        {
            Console.WriteLine("Product ID does not exist.");
            continue;
        }

        // Object by reference.
        deletedProduct = existingProduct;
        isValidProductId = true;
    }

    // Validate confirmation input.
    // You can make this validation into a method for reusability.
    var hasDoneConfirm = false;
    Console.WriteLine("Are you sure want to delete? (Y/N)");
    while (!hasDoneConfirm)
    {
        var confirmationInput = Console.ReadLine();

        if (string.IsNullOrEmpty(confirmationInput))
        {
            Console.WriteLine("Must not empty.");
            continue;
        }

        if (confirmationInput != "Y" && confirmationInput != "N")
        {
            Console.WriteLine("Must choose between Y and N.");
            continue;
        }

        if (confirmationInput == "Y")
        {
            // Remove will remove the item in the list based on the object reference.
            masterProducts!.Remove(deletedProduct);

            Console.WriteLine($"Successfully deleted product with Product ID: {deletedProduct.ProductId}");
        }

        hasDoneConfirm = true;
    }
}

void ViewMasterProductList()
{
    Console.WriteLine("===========================================");
    Console.WriteLine("| No. | Product ID | Name | Price | Quantity |");
    var index = 1;
    foreach (var product in masterProducts)
    {
        Console.WriteLine($"| {index} | {product.ProductId} | {product.Name} | {product.Price} | {product.Quantity} |");

        index++;
    }
    Console.WriteLine("===========================================");
}