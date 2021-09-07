using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimsBolnica
{
    public partial class MainWindow : Window
    {
        App app = (App)Application.Current;
        private List<Medicine> medsDisplay;
        private List<Medicine> medicineList;

        private List<Ingredient> ingredientsDisplay;
        private List<Ingredient> ingredientList;

        private List<Receipt> receiptList;

        private List<User> patientList;

        private Medicine selectedMed;
        private Receipt activeReceipt;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            medicineList = app.medicineController.GetMedicinesByStatus(true);
            ingredientList = app.ingredientController.GetAllIngredients();
            receiptList = app.receiptController.GetPatientReceipts(app.userController.getActiveUserJMBG());
            patientList = app.userController.GetAllPatients();

            activeReceipt = new Receipt(0, "", "", new Dictionary<string, int>(), 0, "");

            copyDisplayMeds();
            copyDisplayIngredients();
            copyDisplayReceipts();
            copyDisplayPatients();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            credentials_warning.Visibility = Visibility.Hidden;

            bool? response = app.userController.login(username_box.Text, password_box.Text);

            if (response == null) showWarning();
            else if (response == false) closeApp();
            else
            {
                hideLoginPage();
                setActiveInterface();
            }
        }

        private void delete_med_button_click(object sender, RoutedEventArgs e)
        {
            if (delete_med_box.Visibility == Visibility.Hidden)
            {
                delete_med_box.Visibility = Visibility.Visible;
            }
            else
            {

                if (((Medicine)medicines_grid.SelectedItem).Id == delete_med_box.Text)
                {
                    app.medicineController.DeleteMedicine(delete_med_box.Text);

                    quantity_box.Visibility = Visibility.Hidden;
                    quantity_box.Text = "";

                    copyDisplayMeds();
                }
                else MessageBox.Show("Potvrda brisanja neuspesna.");
            }
        }

        private void buy_click(object sender, RoutedEventArgs e)
        {
            int qtyToBuy;   
            Int32.TryParse(quantity_box.Text, out qtyToBuy);

            if (quantity_box.Visibility == Visibility.Hidden)
            {
                quantity_box.Visibility = Visibility.Visible;

                selectedMed = (Medicine)medicines_grid.SelectedItem;
            }
            else
            {
                float itemPrice = selectedMed.Price * qtyToBuy;

                if (qtyToBuy <= app.medicineController.GetMaxMedsPerPurchase())
                {
                    activeReceipt.Medicines.Add(selectedMed.Id, qtyToBuy);
                    activeReceipt.Total += itemPrice;

                    app.medicineController.BuyMedicine(selectedMed.Id, qtyToBuy);

                    copyDisplayMeds();

                    quantity_box.Visibility = Visibility.Hidden;
                    quantity_box.Text = "";

                }
                else MessageBox.Show("Maksimalno 5 istih lekova po racunu");
            }
        }

        private void checkout_click(object sender, RoutedEventArgs e)
        {
            if (activeReceipt.Total + app.receiptController.getRecentSpending(app.userController.getActiveUserJMBG()) < app.medicineController.GetMaxMedsPerWeek())
            {
                activeReceipt.PatientJMBG = app.userController.getActiveUserJMBG();
                activeReceipt.DateAndTime = "08.09.2021";

                app.receiptController.CreateReceipt(activeReceipt);
                copyDisplayReceipts();
            }
            else MessageBox.Show("Prekoracili ste dozvoljenu nedeljnu kolicinu lekova");

        }

        private void approve_click(object sender, RoutedEventArgs e)
        {
            app.medicineController.ApproveMedicine(((Medicine)medicines_grid.SelectedItem).Id);
            copyDisplayMeds();
        }

        private void reject_click(object sender, RoutedEventArgs e)
        {
            app.medicineController.RejectMedicine(((Medicine)medicines_grid.SelectedItem).Id);
            copyDisplayMeds();
        }

        private void back_click(object sender, RoutedEventArgs e)
        {
            back_button.Visibility = Visibility.Hidden;

            medicines_grid.Visibility = Visibility.Hidden;

            search_meds_box.Visibility = Visibility.Hidden;
            search_meds_combo.Visibility = Visibility.Hidden;

            buy_meds_button.Visibility = Visibility.Hidden;
            checkout_button.Visibility = Visibility.Hidden;
            quantity_box.Visibility = Visibility.Hidden;

            delete_med_button.Visibility = Visibility.Hidden;
            delete_med_box.Visibility = Visibility.Hidden;

            approve_button.Visibility = Visibility.Hidden;
            reject_button.Visibility = Visibility.Hidden;

            ingredients_grid.Visibility = Visibility.Hidden;

            search_ingredients_box.Visibility = Visibility.Hidden;
            search_ingredients_combo.Visibility = Visibility.Hidden;

            receipts_grid.Visibility = Visibility.Hidden;

            patients_grid.Visibility = Visibility.Hidden;

            name_box.Visibility = Visibility.Hidden;
            surname_box.Visibility = Visibility.Hidden;
            jmbg_box.Visibility = Visibility.Hidden;
            phone_box.Visibility = Visibility.Hidden;
            email_box.Visibility = Visibility.Hidden;
            confirm_reg_button.Visibility = Visibility.Hidden;

            med_name_box.Visibility = Visibility.Hidden;
            med_manufacturer_box.Visibility = Visibility.Hidden;
            med_id_box.Visibility = Visibility.Hidden;
            med_amount_box.Visibility = Visibility.Hidden;
            med_price_box.Visibility = Visibility.Hidden;
            confirm_new_med_button.Visibility = Visibility.Hidden;

            setActiveInterface();
        }

        private void search_meds_combo_change(object sender, RoutedEventArgs e)
        {
            search_meds_box.Text = "";
            copyDisplayMeds();
        }

        private void search_ingredients_combo_change(object sender, RoutedEventArgs e)
        {
            search_ingredients_box.Text = "";
            copyDisplayIngredients();
        }

        private void search_meds_box_changed(object sender, RoutedEventArgs e)
        {
            medsDisplay = new List<Medicine>();
            foreach (Medicine med in medicineList)
            {
                if (search_meds_box.Text != "")
                {
                    bool comparisonBool;
                    string search_string = search_meds_box.Text.ToLower();

                    switch (search_meds_combo.SelectedIndex)
                    {
                        case 0:
                            comparisonBool = med.Id.ToLower().Contains(search_string);
                            break;
                        case 1:
                            comparisonBool = med.Name.ToLower().Contains(search_string);
                            break;
                        case 2:
                            comparisonBool = med.Manufacturer.ToLower().Contains(search_string);
                            break;
                        case 3:
                            comparisonBool = med.Price < Int32.Parse(search_string);
                            break;
                        case 4:
                            comparisonBool = med.Amount > Int32.Parse(search_string);
                            break;
                        case 5:
                            comparisonBool = med.Accepted.ToString().ToLower().Contains(search_string);
                            break;
                        case 6:
                            comparisonBool = med.Deleted.ToString().ToLower().Contains(search_string);
                            break;
                        case 7:
                            comparisonBool = app.medicineController.searchMedByIngredients(med, search_string, 0);
                            break;
                        default:
                            comparisonBool = med.Id.ToLower().Contains(search_string);
                            break;
                    }

                    if (comparisonBool) medsDisplay.Add(med);
                }
                else copyDisplayMeds();
            }
            medicines_grid.ItemsSource = medsDisplay;
        }

        private void search_ingredients_box_changed(object sender, RoutedEventArgs e)
        {
            ingredientsDisplay = new List<Ingredient>();
            foreach (Ingredient ing in ingredientList)
            {
                if (search_ingredients_box.Text != "")
                {
                    bool comparisonBool;
                    string search_string = search_ingredients_box.Text.ToLower();

                    System.Diagnostics.Debug.WriteLine(search_string);

                    switch (search_ingredients_combo.SelectedIndex)
                    {
                        case 0:
                            comparisonBool = ing.Name.ToLower().Contains(search_string);
                            break;
                        case 1:
                            comparisonBool = ing.Description.ToLower().Contains(search_string);
                            break;
                        case 2:
                            comparisonBool = false;
                            System.Diagnostics.Debug.WriteLine(search_string);
                            foreach(Medicine med in app.medicineController.GetAllMedicines())
                            {
                                if(med.Name.ToLower().Contains(search_string))
                                foreach (Ingredient ing2 in med.Ingredients) 
                                {
                                    if (ing2.Name == ing.Name) comparisonBool = true;
                                } 
                            }
                            break;
                        default:
                            comparisonBool = ing.Name.ToLower().Contains(search_string);
                            break;
                    }

                    if (comparisonBool)
                    {
                        ingredientsDisplay.Add(ing);
                    }
                }
                else copyDisplayIngredients();
            }
            ingredients_grid.ItemsSource = ingredientsDisplay;
        }

        private void showWarning()
        {
            credentials_warning.Visibility = Visibility.Visible;
        }

        private void closeApp()
        {
            foreach (var wndOtherWindow in Application.Current.Windows)
            {
                (wndOtherWindow as Window).Close();
            }
        }

        private void setActiveInterface()
        {
            medicines_button.Visibility = Visibility.Visible;
            ingredients_button.Visibility = Visibility.Visible;

            switch (app.userController.getActiveUserType())
            {
                case UserType.patient:
                    receipts_button.Visibility = Visibility.Visible;
                    break;

                case UserType.doctor:
                    patients_button.Visibility = Visibility.Visible;
                    registration_button.Visibility = Visibility.Visible;
                    break;
                case UserType.apothecary:
                    new_med_button.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void hideLoginPage()
        {
            username_box.Visibility = Visibility.Hidden;
            password_box.Visibility = Visibility.Hidden;
            login_button.Visibility = Visibility.Hidden;
        }

        private void hideActiveControls()
        {
            back_button.Visibility = Visibility.Visible;

            medicines_button.Visibility = Visibility.Hidden;
            ingredients_button.Visibility = Visibility.Hidden;

            receipts_button.Visibility = Visibility.Hidden;

            patients_button.Visibility = Visibility.Hidden;

            registration_button.Visibility = Visibility.Hidden;
            
            new_med_button.Visibility = Visibility.Hidden;
        }

        private void medicines_click(object sender, RoutedEventArgs e)
        {
            copyDisplayMeds();

            hideActiveControls();

            medicines_grid.Visibility = Visibility.Visible;

            search_meds_box.Visibility = Visibility.Visible;
            search_meds_combo.Visibility = Visibility.Visible;

            switch (app.userController.getActiveUserType())
            {
                case UserType.patient:
                    buy_meds_button.Visibility = Visibility.Visible;
                    checkout_button.Visibility = Visibility.Visible;
                    medicines_grid.Columns[5].Visibility = Visibility.Hidden;
                    medicines_grid.Columns[6].Visibility = Visibility.Hidden;
                    ((ComboBoxItem)search_meds_combo.Items[5]).Visibility = Visibility.Collapsed;
                    ((ComboBoxItem)search_meds_combo.Items[6]).Visibility = Visibility.Collapsed;
                    break;

                case UserType.doctor:
                    medicines_grid.Columns[6].Visibility = Visibility.Hidden;
                    ((ComboBoxItem)search_meds_combo.Items[6]).Visibility = Visibility.Collapsed;
                    approve_button.Visibility = Visibility.Visible;
                    reject_button.Visibility = Visibility.Visible;
                    break;

                case UserType.apothecary:
                    delete_med_button.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void ingredients_click(object sender, RoutedEventArgs e)
        {
            copyDisplayIngredients();

            hideActiveControls();

            ingredients_grid.Visibility = Visibility.Visible;
            search_ingredients_box.Visibility = Visibility.Visible;
            search_ingredients_combo.Visibility = Visibility.Visible;
        }

        private void receipts_click(object sender, RoutedEventArgs e)
        {
            copyDisplayReceipts();

            hideActiveControls();

            receipts_grid.Visibility = Visibility.Visible;
        }

        private void patients_click(object sender, RoutedEventArgs e)
        {
            copyDisplayPatients();

            hideActiveControls();

            patients_grid.Visibility = Visibility.Visible;
        }

        private void registration_click(object sender, RoutedEventArgs e)
        {

            hideActiveControls();

            name_box.Visibility = Visibility.Visible;
            surname_box.Visibility = Visibility.Visible;
            jmbg_box.Visibility = Visibility.Visible;
            phone_box.Visibility = Visibility.Visible;
            email_box.Visibility = Visibility.Visible;

            confirm_reg_button.Visibility = Visibility.Visible;
        }

        private void confirm_reg_click(object sender, RoutedEventArgs e)
        {
            User patient = new User(jmbg_box.Text, email_box.Text, name_box.Text, surname_box.Text, phone_box.Text, UserType.patient);

            if (app.userController.RegisterPatient(patient))
            {
                name_box.Text = "";
                surname_box.Text = "";
                jmbg_box.Text = "";
                phone_box.Text = "";
                email_box.Text = "";
            }
        }

        private void new_med_click(object sender, RoutedEventArgs e)
        {
            hideActiveControls();

            med_name_box.Visibility = Visibility.Visible;
            med_id_box.Visibility = Visibility.Visible;
            med_amount_box.Visibility = Visibility.Visible;
            System.Diagnostics.Debug.WriteLine("AAA");
            med_price_box.Visibility = Visibility.Visible;
            med_manufacturer_box.Visibility = Visibility.Visible;
            confirm_new_med_button.Visibility = Visibility.Visible;
        }

        private void confirm_new_med_click(object sender, RoutedEventArgs e)
        {
            Medicine med = new Medicine(med_id_box.Text, med_name_box.Text, med_manufacturer_box.Text, float.Parse(med_price_box.Text), Int32.Parse(med_amount_box.Text), new List<Ingredient>(), false, false);

            app.medicineController.CreateMedicine(med);
            
            med_name_box.Text = "";
            med_manufacturer_box.Text = "";
            med_id_box.Text = "";
            med_amount_box.Text = "";
            med_price_box.Text = "";
            
        }

        private void copyDisplayMeds()
        {
            refreshMedList();

            medsDisplay = new List<Medicine>();
            foreach (Medicine med in medicineList)
            {
                medsDisplay.Add(med);
            }
            medicines_grid.ItemsSource = medsDisplay;
        }

        private void refreshMedList()
        {
            switch (app.userController.getActiveUserType())
            {
                case UserType.patient:
                    medicineList = app.medicineController.GetMedicinesByStatus(true);
                    break;
                case UserType.doctor:
                    medicineList = app.medicineController.GetUndeletedMedicines();
                    break;
                case UserType.apothecary:
                    medicineList = app.medicineController.GetAllMedicines();
                    break;
                default:
                    break;
            }
        }

        private void copyDisplayIngredients()
        {
            ingredientList = app.ingredientController.GetAllIngredients();
            ingredientsDisplay = new List<Ingredient>();
            foreach (Ingredient ing in ingredientList)
            {
                ingredientsDisplay.Add(ing);
            }
            ingredients_grid.ItemsSource = ingredientsDisplay;
        }

        private void copyDisplayReceipts()
        {
            receiptList = app.receiptController.GetPatientReceipts(app.userController.getActiveUserJMBG());
            receipts_grid.ItemsSource = receiptList;
        }

        private void copyDisplayPatients()
        {
            patientList = app.userController.GetAllPatients();
            patients_grid.ItemsSource = patientList;
        }
    }
}
