using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace CarInventory
{
    public partial class Form1 : Form
    {
        List<Car> cars = new List<Car>();

        public Form1()
        {
            InitializeComponent();
            LoadDB();
            showList();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                string carYear = yearInput.Text;
                string carMake = makeInput.Text;
                string carColour = colourInput.Text;
                string carMileage = mileageInput.Text;


                Car newCar = new Car(carYear, carMake, carColour, carMileage);

                cars.Add(newCar);
            }
            catch
            {

            }
            showList();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            string make = makeInput.Text;
            string year = yearInput.Text;
            string mileage = mileageInput.Text;
            string colour = colourInput.Text;
 
            foreach(Car newCar in cars)
            {
                if (newCar.make == make && newCar.colour == colour && newCar.mileage == mileage && newCar.year == year)
                {
                    cars.Remove(newCar);
                    break;
                }
            }
            showList();
        }
        private void showList()
        {
            outputLabel.Text = "";

            foreach(Car newCar in cars)
            {
                outputLabel.Text += $"Year: {newCar.year}  Make: {newCar.make}  Colour: {newCar.colour}  Mileage: {newCar.mileage}\n";
            }

            yearInput.Text = "";
            mileageInput.Text = "";
            colourInput.Text = "";
            makeInput.Text = "";
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveDB();
        }

        private void SaveDB()
        {
            XmlWriter writer = XmlWriter.Create("Resources/CarData.xml", null);

            writer.WriteStartElement("Cars");
            foreach (Car car in cars)
            {
                writer.WriteStartElement("Car");

                writer.WriteElementString("Make", car.make);
                writer.WriteElementString("Year", car.year);
                writer.WriteElementString("Mileage", car.mileage);
                writer.WriteElementString("Colour", car.colour);

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            writer.Close();
        }

        private void LoadDB()
        {
            XmlReader reader = XmlReader.Create("Resources/CarData.xml");
            while(reader.Read())
            {
                if(reader.NodeType == XmlNodeType.Text)
                {
                    string make = reader.ReadString();

                    reader.ReadToNextSibling("Year");
                    string year = reader.ReadString();

                    reader.ReadToNextSibling("Mileage");
                    string mileage = reader.ReadString();

                    reader.ReadToNextSibling("Colour");
                    string colour = reader.ReadString();

                    cars.Add(new Car(year, make, colour, mileage));
                }
            }

            reader.Close();  
        }
    }


}
