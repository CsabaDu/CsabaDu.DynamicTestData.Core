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

![CsabaDu_DynamicTestData_Segregated_Simplified](https://raw.githubusercontent.com/CsabaDu/CsabaDu.DynamicTestData/refs/heads/master/_Images/CsabaDu_DynamicTestData_Segregated_Simplified.svg)


### **Modules and contents**

#### **Core Foundation Module (package: `CsabaDu.DynamicTestData.Core`)**  

Foundation layer with essential contract surface, DTOs, stateless encoding helpers and strategy definitions.

##### Namespaces and highlights:  

---
**`CsabaDu.DynamicTestData.Statics`**  

- *Encoding enums for data strategy*:  

  - **ArgsCoce.cs**  
	
	- `ArgsCode` : enum  

  - **PropsCode.cs**  
	
	- `PropsCode` : enum  
 
- *Helper methods*:  

  - **Extensions.cs**  
	
	- `Extensions` : static class

---
**`CsabaDu.DynamicTestData.TestDataTypes.Interfaces`**  

- *Testcase name contract*:  

  - **INamedTestCase.cs**  
	
	- `INamedTestCase` : interface  

- *DTO contracts*:  

  - **ITestData.cs**  
	
	- `ITestData` : interface
	- `ITestData<TResult>` : interface
	- `ITestData<TResult, T1 ... T9>` : interfaces

- *Specialization Markers*:  

  - **IExpected.cs**  
	
	- `IExpected` : interface  

  - **ITestDataReturns.cs**  
	
	- `ITestDataReturns` : interface,  
	- `ITestDataReturns<TStruct>` : interface,  

  - **ITestDataThrows.cs**  
	
	- `ITestDataThrows` : interface,  
	- `ITestDataThrows<TException>` : interface,  

---
**`CsabaDu.DynamicTestData.TestDataTypes`**  

- *DTO record types*:  

  - **TestData.cs**  
	
	- `TestData` : abstract record,
	- `TestData<T1 ... T9>` : records,

  - **TestDataReturns.cs**  
	
	- `TestDataReturns<TStruct>` : abstract record,
	- `TestDataReturns<TStruct, T1 ... T9>` : records,

  - **TestDataThrows.cs**  
	
	- `TestDataThrows<TException>` : abstract record,
	- `TestDataThrows<TException, T1 ... T9>` : records,

- *Factory*:  

  - **TestDataFactory.cs**  
	
	- `TestDataFactory` : static class

---
##### When to use:  

- Implementing framework adapters or conversion strategies that only need contracts and DTOs
- Authoring manual `IEnumerable<object[]>` producers that require extra metadata (display names, expected/throw semantics) without pulling runtime rows/holders
- Keeping a minimal dependency footprint in libraries and tools

##### Dependencies: none

---

## Changelog

### **Version 2.1.0-beta** (2025-10-26)

- Initial release of the `CsabaDu.DynamicTestData.Core` framework, by segregating core types of the `CsabaDu.DynamicTestData` framework into a dedicated package.

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
