# 💳 MiniBankingSystem

A simple in-memory banking system built with .NET 8, designed as a prototype backend for managing customer accounts and transactions.

---

## 📌 Project Overview

This system provides core banking functionalities such as:

- Creating bank accounts
- Depositing and withdrawing funds
- Transferring money between accounts
- Viewing account transaction history
- Preventing overdrafts (no negative balances allowed)

Built using clean architecture principles with proper separation of concerns between domain, application, and infrastructure layers.

---

## ⚙️ Setup Instructions

### 🔧 Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- Git (optional)

### 🚀 Running the API

```bash
git clone https://github.com/Afsanehonarmand/MiniBankingSystem.git
cd MiniBankingSystem
dotnet run --project MiniBankingSystem.App
