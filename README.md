# 💰 Interest Calculator (C# WinForms)

## 🧩 Overview
**Interest Calculator** is a simple Windows Forms application written in **C# (.NET Framework)** that allows users to calculate **simple** and **compound interest** for **daily**, **monthly**, and **yearly** periods.  
It’s designed as a beginner-friendly project demonstrating control operators, event handling, and GUI design in C# WinForms.

---

## ⚙️ Features
✅ Calculates both **simple** and **compound interest**  
✅ Supports **daily**, **monthly**, and **yearly** interest computation  
✅ Customizable **days** and **months per year**  
✅ Dynamic **DataGridView** results table  
✅ Easy **Clear** and **Exit** buttons  
✅ Intuitive tooltips and clean UI  

---

## 🧮 Formula Reference
- **Simple Interest**  
  A = P × (1 + r)
- **Compound Interest**  
  A = P × (1 + r/m)^k

Where:  
- P = Principal amount  
- r = Annual interest rate (as decimal)  
- m = Number of compounding periods per year  
- k = Number of periods  

---

## 🪟 User Interface
**Main form contains:**
- Input fields for **Amount (P)** and **Annual Interest Rate (%)**
- Numeric selectors for **Days in year** and **Months in year**
- Buttons:
  - **Calculate** – Performs the calculation and displays results  
  - **Clear** – Resets all inputs and clears the table  
  - **Exit** – Closes the application  
- A **DataGridView** showing:
  | Period | Principal (P) | Simple Interest Amount | Compound Interest Amount | Compound Interest Earned |

---

## 🧰 Technologies Used
- **Language:** C#  
- **Framework:** .NET Framework 4.7.2 (or later)  
- **IDE:** Visual Studio 2019 / 2022  
- **UI Toolkit:** Windows Forms (WinForms)

---

## 🧑‍💻 Installation & Run
1. Clone this repository:
   git clone https://github.com/<your-username>/InterestCalculatorWinForms.git

2. Open the solution in **Visual Studio**.  
3. Build the project (Ctrl + Shift + B).  
4. Run (F5) to launch the Interest Calculator.  

---

## 📁 Project Structure
InterestCalculatorWinForms/

│

├── Form1.cs      // Main form logic (UI + calculation)

├── Program.cs   // Entry point of the application

├── App.config  // Application configuration (auto-generated)

├── InterestCalculatorWinForms.csproj  // Project file

└── README.md             # Project documentation


---

## 📘 License
This project is open-source and available under the **MIT License**.

---

## ✨ Author
Developed by **Kristina Mateva**
