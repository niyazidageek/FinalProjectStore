using FinalProjectStore.Services;
using FinalProjectStore.Data;
using System;
using FinalProjectStore.Data.Entities;
namespace FinalProjectStore
{
    class Program
    {
        static void Main(string[] args)
        {
            int selection = 0;
            int option = 0;
            int option2 = 0;
            do
            {
                Console.WriteLine("================Welcome to the store================");
                Console.WriteLine("1. Work on products");
                Console.WriteLine("2. Work on invoices");
                Console.WriteLine("0. Exit");
                Console.WriteLine("Select an option, please");
                string selectionstr = Console.ReadLine();
                while (!int.TryParse(selectionstr, out selection))
                {
                    Console.WriteLine("Enter a number, please!");
                    selectionstr = Console.ReadLine();
                }
                switch (selection)
                {
                    case 1:
                        do
                        {

                            Console.WriteLine("1. Add product");
                            Console.WriteLine("2. Edit product");
                            Console.WriteLine("3. Delete product");
                            Console.WriteLine("4. Show all the products");
                            Console.WriteLine("5. Show products by category");
                            Console.WriteLine("6. Show products by price range");
                            Console.WriteLine("7. Show products by name");
                            Console.WriteLine("0. Back");
                            Console.WriteLine("Select an option, please");
                            string optionstr = Console.ReadLine();
                            while (!int.TryParse(optionstr, out option))
                            {
                                Console.WriteLine("Enter a number, please!");
                                optionstr = Console.ReadLine();
                            }
                            switch (option)
                            {
                                case 1:
                                    MenuService.AddProductMenu();
                                    break;
                                case 2:
                                    MenuService.AddChangeProductMenu();
                                    break;
                                case 3:
                                    MenuService.AddRemoveProductMenu();
                                    break;
                                case 4:
                                    MenuService.DisplayProducts();
                                    break;
                                case 5:
                                    MenuService.DisplayProductsByCategory();
                                    break;
                                case 6:
                                    MenuService.DisplayProductByPrice();
                                    break;
                                case 7:
                                    MenuService.DisplayProductByName();
                                    break;
                                case 0:
                                    
                                    break;
                                default:
                                    Console.WriteLine("There is no such option");
                                    break;
                            }
                        } while (option != 0);
                            break;
                    case 2:
                        do
                        {

                            Console.WriteLine("1. Add invoice");
                            Console.WriteLine("2. Return invoice");
                            Console.WriteLine("3. Delete invoice");
                            Console.WriteLine("4. Show all the invoices");
                            Console.WriteLine("5. Show invoices by date range");
                            Console.WriteLine("6. Show invoices by cost range");
                            Console.WriteLine("7. Show invoice by exact date");
                            Console.WriteLine("8. Show invoice by number");
                            Console.WriteLine("0. Back");
                            Console.WriteLine("Select an option, please");
                            string optionstr2 = Console.ReadLine();
                            while (!int.TryParse(optionstr2, out option2))
                            {
                                Console.WriteLine("Enter a number, please!");
                                optionstr2 = Console.ReadLine();
                            }
                            switch (option2)
                            {
                                case 1:
                                    MenuService.AddInvoiceMenu();
                                    break;
                                case 2:
                                    MenuService.AddReturnProductMenu();
                                    break;
                                case 3:
                                    MenuService.AddDeleteInvoiceMenu();
                                    break;
                                case 4:
                                    MenuService.DisplayInvoices();
                                    break;
                                case 5:
                                    MenuService.DisplayInvoicesByDate();
                                    break;
                                case 6:
                                    MenuService.DisplayInvoiceByCost();
                                    break;
                                case 7:
                                    MenuService.DisplayInvoiceByOnlyDate();
                                    break;
                                case 8:
                                    MenuService.DisplayInvoiceByNo();
                                    break;
                                case 0:
                                    
                                    break;
                                default:
                                    Console.WriteLine("There is no such option");
                                    break;
                            }
                        } while (option2 != 0);
                        break;
                    case 0:
                        Console.WriteLine("Good bye!");
                        break;
                    default:
                        Console.WriteLine("There is no such option");
                        break;
                }
            } while (selection != 0);
            

        }
    }
}
