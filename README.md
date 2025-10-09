# 🌾 Harvestly - Fresh Produce E-Commerce Platform

A modern, professional e-commerce web application built with ASP.NET MVC for connecting local farmers with consumers, promoting fresh, organic produce delivery.

## 🚀 Features

### 🛒 **E-Commerce Functionality**
- **Product Catalog**: Browse fresh, organic produce from local farms
- **Shopping Cart**: Add products to cart with quantity management
- **Order Management**: Complete order processing and tracking
- **User-Friendly Interface**: Intuitive navigation and responsive design

### 👨‍🌾 **Farmer Dashboard**
- **Product Management**: Add, edit, and delete farm products
- **Inventory Tracking**: Monitor product quantities and availability
- **Order Processing**: View and manage customer orders
- **Analytics Dashboard**: Track sales and performance metrics

### 🎨 **Modern UI/UX**
- **Responsive Design**: Works seamlessly on desktop, tablet, and mobile
- **Professional Styling**: Clean, modern interface with green agriculture theme
- **Interactive Elements**: Smooth animations and hover effects
- **Accessibility**: User-friendly navigation and clear visual hierarchy

## 🛠️ Technology Stack

- **Backend**: ASP.NET MVC 5.2.4
- **Database**: Entity Framework 6.4.0 with Code First migrations
- **Frontend**: HTML5, CSS3, JavaScript, Bootstrap 3.3.7
- **Styling**: Custom CSS with modern design principles
- **Icons**: Font Awesome 6.0.0
- **Fonts**: Google Fonts (Poppins)

## 📋 Prerequisites

- Visual Studio 2019 or later
- .NET Framework 4.7.2 or later
- SQL Server (LocalDB or Express)
- IIS Express (included with Visual Studio)

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/Harvestly.git
cd Harvestly
```

### 2. Open in Visual Studio
- Open `Harvestly.sln` in Visual Studio
- Restore NuGet packages if prompted

### 3. Database Setup
- The application uses Entity Framework Code First migrations
- Database will be created automatically on first run
- Connection string is configured in `Web.config`

### 4. Run the Application
- Press F5 or click "Start Debugging"
- Navigate to the URL shown in the browser (typically `http://localhost:port`)

## 📁 Project Structure

```
Harvestly/
├── Controllers/           # MVC Controllers
│   ├── HomeController.cs
│   ├── CartsController.cs
│   └── OrdersController.cs
├── Models/               # Data Models
│   ├── MenuItem.cs
│   ├── Cart.cs
│   ├── Order.cs
│   └── HarvestlyContext.cs
├── Views/               # Razor Views
│   ├── Home/
│   ├── Cart/
│   └── Orders/
├── Content/             # CSS and Static Files
│   └── Site.css
├── Scripts/             # JavaScript Files
└── Migrations/          # Entity Framework Migrations
```

## 🎯 Key Features Implemented

### **E-Commerce Core**
- ✅ Product catalog with categories
- ✅ Shopping cart functionality
- ✅ Order processing and management
- ✅ User authentication ready

### **Admin Panel**
- ✅ Product management (CRUD operations)
- ✅ Order tracking and management
- ✅ Inventory management
- ✅ Sales analytics dashboard

### **Modern Design**
- ✅ Responsive Bootstrap layout
- ✅ Professional color scheme
- ✅ Interactive UI components
- ✅ Mobile-optimized interface

## 🎨 Design Highlights

- **Clean, Professional Interface**: Modern card-based layout
- **Agriculture Theme**: Green color palette representing fresh produce
- **Responsive Design**: Optimized for all device sizes
- **User Experience**: Intuitive navigation and clear call-to-actions
- **Accessibility**: Proper contrast ratios and semantic HTML

## 📊 Database Schema

### **Core Entities**
- **MenuItem**: Product information (name, price, quantity, category)
- **Cart**: Shopping cart items
- **Order**: Customer orders with shipping information
- **Category**: Product categorization

### **Key Relationships**
- MenuItem belongs to Category
- Cart contains MenuItems
- Order contains customer and shipping details

## 🔧 Configuration

### **Connection String**
Update the connection string in `Web.config`:
```xml
<connectionStrings>
  <add name="HarvestlyContext" 
       connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=HarvestlyDB;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### **App Settings**
Configure application settings in `Web.config`:
```xml
<appSettings>
  <add key="EnableSSL" value="false" />
</appSettings>
```

## 📈 Future Enhancements

- [ ] User authentication and authorization
- [ ] Payment gateway integration
- [ ] Email notifications
- [ ] Advanced search and filtering
- [ ] Product reviews and ratings
- [ ] Inventory alerts
- [ ] Mobile app development
- [ ] API development for mobile integration

---

<div align="center">
  <strong>🌾 Supporting Local Farmers, Delivering Fresh Produce 🌾</strong>
</div>
