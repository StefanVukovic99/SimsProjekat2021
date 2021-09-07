using Controller;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimsBolnica
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly IngredientRepository ingredientRepository = new IngredientRepository();
        private static readonly MedicineRepository medicineRepository = new MedicineRepository();
        private static readonly ReceiptRepository receiptRepository = new ReceiptRepository();
        private static readonly UserRepository userRepository = new UserRepository();

        private static readonly IngredientService ingredientService = new IngredientService(ingredientRepository);
        private static readonly UserService userService = new UserService(userRepository);
        private static readonly MedicineService medicineService = new MedicineService(medicineRepository);
        private static readonly ReceiptService receiptService = new ReceiptService(receiptRepository);

        public readonly IngredientController ingredientController = new IngredientController(ingredientService);
        public readonly UserController userController = new UserController(userService);
        public readonly MedicineController medicineController = new MedicineController(medicineService);
        public readonly ReceiptController receiptController = new ReceiptController(receiptService);
    }
}
