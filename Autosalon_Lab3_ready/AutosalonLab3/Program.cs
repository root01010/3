using System;
using System.Collections.Generic;
using System.Globalization;

namespace AutosalonLab3
{
    class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        public Car(string brand, string model, int year, double price)
        {
            Brand = brand;
            Model = model;
            Year = year;
            Price = price;
        }
    }

    class Program
    {
        static List<Car> cars = new List<Car>()
        {
            new Car("Toyota", "Camry", 2021, 24500),
            new Car("BMW", "X5", 2020, 52000),
            new Car("Audi", "A6", 2019, 37000),
            new Car("Tesla", "Model 3", 2022, 41000),
            new Car("Volkswagen", "Golf", 2018, 15500)
        };

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            int choice;
            do
            {
                ShowMainMenu();
                choice = ReadInt("Оберіть пункт меню: ");

                switch (choice)
                {
                    case 1:
                        ShowCars();
                        break;
                    case 2:
                        AddCar();
                        break;
                    case 3:
                        SearchByMaxPrice();
                        break;
                    case 4:
                        ShowStatistics();
                        break;
                    case 5:
                        ShowCarsByYear();
                        break;
                    case 0:
                        PrintColor("Вихід з програми...", ConsoleColor.Yellow);
                        break;
                    default:
                        PrintColor("Помилка: такого пункту меню немає!", ConsoleColor.Red);
                        break;
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nНатисніть Enter для продовження...");
                    Console.ReadLine();
                }
            }
            while (choice != 0);
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========== АВТОСАЛОН ==========");
            Console.ResetColor();
            Console.WriteLine("1. Переглянути всі автомобілі");
            Console.WriteLine("2. Додати автомобіль");
            Console.WriteLine("3. Пошук авто за максимальною ціною");
            Console.WriteLine("4. Статистика автосалону");
            Console.WriteLine("5. Вивести авто новіші за вказаний рік");
            Console.WriteLine("0. Вихід");
            Console.WriteLine("================================");
        }

        static void ShowCars()
        {
            PrintColor("\nСписок автомобілів:", ConsoleColor.Green);

            for (int i = 0; i < cars.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cars[i].Brand} {cars[i].Model}, {cars[i].Year} р., {cars[i].Price:F2} $");
            }
        }

        static void AddCar()
        {
            Console.Write("Введіть марку: ");
            string brand = Console.ReadLine() ?? "Невідомо";

            Console.Write("Введіть модель: ");
            string model = Console.ReadLine() ?? "Невідомо";

            int year = ReadInt("Введіть рік випуску: ");
            double price = ReadDouble("Введіть ціну авто: ");

            cars.Add(new Car(brand, model, year, price));
            PrintColor("Автомобіль успішно додано!", ConsoleColor.Green);
        }

        static void SearchByMaxPrice()
        {
            double maxPrice = ReadDouble("Введіть максимальну ціну: ");
            bool found = false;

            PrintColor("\nАвтомобілі, що підходять за ціною:", ConsoleColor.Green);

            foreach (Car car in cars)
            {
                if (car.Price > maxPrice)
                {
                    continue; // пропускаємо авто, які дорожчі за введену суму
                }

                Console.WriteLine($"{car.Brand} {car.Model}, {car.Year} р., {car.Price:F2} $");
                found = true;
            }

            if (!found)
            {
                PrintColor("Автомобілів за такою ціною не знайдено.", ConsoleColor.Red);
            }
        }

        static void ShowStatistics()
        {
            if (cars.Count == 0)
            {
                PrintColor("Список автомобілів порожній.", ConsoleColor.Red);
                return;
            }

            double total = 0;
            double minPrice = cars[0].Price;
            double maxPrice = cars[0].Price;

            int i = 0;
            while (i < cars.Count)
            {
                total += cars[i].Price;

                if (cars[i].Price < minPrice)
                    minPrice = cars[i].Price;

                if (cars[i].Price > maxPrice)
                    maxPrice = cars[i].Price;

                i++;
            }

            double average = total / cars.Count;

            PrintColor("\nСтатистика автосалону:", ConsoleColor.Green);
            Console.WriteLine($"Кількість автомобілів: {cars.Count}");
            Console.WriteLine($"Загальна вартість авто: {total:F2} $");
            Console.WriteLine($"Середня ціна авто: {average:F2} $");
            Console.WriteLine($"Мінімальна ціна: {minPrice:F2} $");
            Console.WriteLine($"Максимальна ціна: {maxPrice:F2} $");
        }

        static void ShowCarsByYear()
        {
            int year = ReadInt("Показати авто новіші за рік: ");
            int index = 0;
            bool found = false;

            PrintColor("\nРезультат пошуку:", ConsoleColor.Green);

            do
            {
                if (cars.Count == 0)
                {
                    break;
                }

                if (cars[index].Year > year)
                {
                    Console.WriteLine($"{cars[index].Brand} {cars[index].Model}, {cars[index].Year} р., {cars[index].Price:F2} $");
                    found = true;
                }

                index++;
            }
            while (index < cars.Count);

            if (!found)
            {
                PrintColor("Автомобілів новіших за цей рік не знайдено.", ConsoleColor.Red);
            }
        }

        static int ReadInt(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int value))
                    return value;

                PrintColor("Введіть ціле число!", ConsoleColor.Red);
            }
        }

        static double ReadDouble(string message)
        {
            while (true)
            {
                Console.Write(message);
                string? input = Console.ReadLine();

                if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                    return value;

                PrintColor("Введіть число у правильному форматі!", ConsoleColor.Red);
            }
        }

        static void PrintColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
