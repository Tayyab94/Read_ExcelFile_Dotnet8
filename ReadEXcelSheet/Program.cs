// See https://aka.ms/new-console-template for more information

using ExcelDataReader;
using ReadEXcelSheet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Text;
Console.OutputEncoding = System.Text.Encoding.UTF8;

string pathToExcelFile = @"C:\Users\tayya\Desktop\javascript-youtube-main\PythonPractice\Book1.xlsx";

List<User> users = ReadExcelData(pathToExcelFile);

foreach (var user in users)
{
    Console.WriteLine($"Item ID: {user.ItemId}, Item Name: {user.ItemName}, Urdu Name: {user.UrduName}");

    using (var context = new DataContext())
    {
        // Add users to the database

        var data = new User
        {
            ItemId = user.ItemId,
            ItemName = user.ItemName,
            UrduName = user.UrduName,
        };

        context.Users.Add(data);
        context.SaveChanges();
       
    }
    Console.WriteLine($"Record Added {user.Id}");
}


Console.WriteLine("All Date Saved");
Console.ReadKey();

static List<User> ReadExcelData(string filePath)
{
    List<User> users = new List<User>();

    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
    {
        using (var reader = ExcelReaderFactory.CreateReader(stream, new ExcelReaderConfiguration()
        {
            FallbackEncoding = Encoding.GetEncoding(1256), // Set the encoding for Urdu (Windows-1256 for Urdu)
           
        }))
        {
            do
            {
                int rowIndex = 0;
                // To specify the sheet name (as you know we can have mutiple sheets in 1 excel file. 
                if (reader.Name == "ProductSheet")
                    {
                         // Initialize a counter for the rows

                        while (reader.Read())
                        {
                            if (rowIndex > 0)
                            {
                                // Assuming ItemId is in the first column, ItemName in the second, and UrduName in the third
                                User user = new User
                                {
                                    ItemId = reader.GetValue(0)?.ToString(), // Assuming ItemId is in the first column
                                    ItemName = reader.GetValue(1)?.ToString(), // Assuming ItemName is in the second column
                                    UrduName = reader.GetString(2) // Assuming UrduName is in the third column (index 2)
                                };
                                users.Add(user);
                            }
                            rowIndex++;

                        }
                    }
                else
                {
                    // Initialize a counter for the rows

                    while (reader.Read())
                    {
                        if (rowIndex > 0)
                        {
                            // Assuming ItemId is in the first column, ItemName in the second, and UrduName in the third
                            User user = new User
                            {
                                ItemId = reader.GetValue(1)?.ToString(), // Assuming ItemId is in the first column
                                ItemName = reader.GetValue(2)?.ToString(), // Assuming ItemName is in the second column
                                UrduName = reader.GetString(3) // Assuming UrduName is in the third column (index 2)
                            };
                            users.Add(user);
                        }
                        rowIndex++;

                    }
                }
               
            } while (reader.NextResult()); // Move to the next sheet if available
        }
    }

    return users;
}


public class User
{
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [StringLength(50)]
    public string ItemId { get; set; }

    [StringLength(500)]
    public string ItemName { get; set; }
    [StringLength(500)]
    public string UrduName { get; set; }
}
