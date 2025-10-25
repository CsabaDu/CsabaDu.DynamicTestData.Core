# CsabaDu.DynamicTestData.Core

Core types of **CsabaDu.DynamicTestData**, a robust, flexible, extensible, pure .NET framework to facilitate dynamic data-driven testing.

---

## 📖 Documentation

This README contains the base info and the current version related notes.    
Visit the **[Wiki](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki)** for full documentation, including  
- 📖 [**Introduction**](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki/00-%F0%9F%93%96-Introduction)
- 🚀 [**Quick Start Guide**](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki/01-%F0%9F%9A%80-Quick-Start-Guide)  
- 📐 [**Architecture**](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki/02-%F0%9F%93%90-Architecture)  
- 🔍 [**Types**](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki/03-%F0%9F%94%8D-Types)  
- 🌍 [**Project Ecosystem**](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki/04-%F0%9F%8C%8D-Project-Ecosystem)  
- 📚 [**Sample Codes**](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki/05-%F0%9F%93%9A-Sample-Codes)  

---

## 📘 Table of Contents

- [**CsabaDu.DynamicTestData — Modular Architecture**](#csabadudynamictestdata--modular-architecture))
- [**Changelog**](#changelog)
- [**Contributing**](#contributing)
- [**License**](#license)
- [**Contact**](#contact)
- [**FAQ**](#faq)
- [**Troubleshooting**](#troubleshooting)

---

## CsabaDu.DynamicTestData — Modular Architecture

### **Overview**  

**CsabaDu.DynamicTestData** has been reorganized from a single monolithic package into a set of focused, aligned modules (NuGet packages) while keeping a clean, consistent namespace hierarchy under `CsabaDu.DynamicTestData.*`. Modules are deployable package boundaries; namespaces are logical organization inside those packages. The new layout reduces transitive dependencies, clarifies responsibilities, and makes it easier for consumers to adopt only what they need.

See the Segregated Architecture Diagram for a visual overview of module boundaries and namespace relationships:

![CsabaDu_DynamicTestData_Segregated_Simplified](https://raw.githubusercontent.com/CsabaDu/CsabaDu.DynamicTestData/refs/heads/Core_Lite/_Images/CsabaDu_DynamicTestData_Segregated_Simplified.svg)


### **Modules and contents**

**Core Foundation Module (package: `CsabaDu.DynamicTestData.Core`)**  

Essential types and interfaces — foundation for other modules.

Namespaces and highlights: 
- `CsabaDu.DynamicTestData.Statics`  
  - Extensions.cs — utility extension methods and small helpers  

- `CsabaDu.DynamicTestData.TestDataTypes`  
  - `Interfaces` — core contracts:
	- *IExpected.cs*
	- *INamedTestCase.cs*
	- *ITestData.cs*
	- *ITestDataReturns.cs*
	- *ITestDataThrows.cs*
  - Concrete helpers:
	- *TestData.cs*
	- *TestDataReturns.cs*
	- *TestDataThrows.cs*
	- *TestDataFactory.cs*

Purpose: provide the smallest possible dependency surface for library authors, adapters and implementers who only need contracts and DTOs.

---

## Changelog

---

## License

This project is licensed under the MIT License. See the [License](LICENSE.txt) file for details.

---

## Contact

For any questions or inquiries, please contact [CsabaDu](https://github.com/CsabaDu).

---

## FAQ
---

## Troubleshooting
