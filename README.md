# CsabaDu.DynamicTestData.Core

Core types of **CsabaDu.DynamicTestData**, a robust, flexible, extensible, pure .NET framework to facilitate dynamic data-driven testing.

---
## **Modular Design Overview**  

**CsabaDu.DynamicTestData** has been reorganized from a single monolithic package into a set of focused, aligned modules (NuGet packages) while keeping a clean, consistent namespace hierarchy under `CsabaDu.DynamicTestData.*`. Modules are deployable package boundaries; namespaces are logical organization inside those packages. The new layout reduces transitive dependencies, clarifies responsibilities, and makes it easier for developers to adopt only what they need.

See the Segregated Architecture Diagram for a visual overview of module and namespace boundaries and dependencies:

![CsabaDu_DynamicTestData_Segregated_Simplified_Full](https://raw.githubusercontent.com/CsabaDu/CsabaDu.DynamicTestData/refs/heads/master/_Images/CsabaDu_DynamicTestData_Segregated_Simplified_Full.svg)

---

## Purpose  

**CsabaDu.DynamicTestData.Core** provides the **foundational architecture** for type‑safe, maintainable test data in .NET. Solves the challenge of keeping inputs, expected outcomes, and exception scenarios consistent across diverse test suites.  

---

## Key Innovations  
- **DTO‑based type hierarchy** — transports test case info safely across framework layers  
- **Automatic test case naming** — descriptive, human‑readable names for parameterized tests  
- **Configurable strategy enums** — flexible row representation (instances vs. properties)  
- **Thread‑safe design** — Memento pattern + AsyncLocal ensures isolation in parallel runs  

---

## Four‑Layer Model  

| Layer | Role | Example |
|-------|------|---------|
| **Base Interfaces** | Universal access across all test types | `ITestData`, `INamedTestCase` |
| **Markers** | Intent discovery & pattern matching | `IExpected`, `ITestDataReturns` `ITestDataThrows` |
| **Generic Constraints** | Compile‑time validation | `ITestData<TResult, T1..T9>`, `ITestDataReturns<TStruct>` `ITestDataThrows<TException>` with constraints |
| **Concrete Implementations** | Type‑safe operations with context | `TestData<T1..T9>` `TestDataReturns<TStruct, T1..T9>`, `TestDataThrows<TExeption, T1..T9>` |

---

## Benefits  
- **Zero dependencies** — pure .NET, framework‑agnostic  
- **Type‑safe composition** — compile‑time validation of expectations  
- **Clear intent signaling** — specialized interfaces (standard, returns, throws)  
- **Automatic traceability** — descriptive names eliminate ambiguity  
- **Integration ready** — extends xUnit, NUnit, MSTest seamlessly  

---

## Limitations  
- **Learning curve** — advanced generics and patterns require expertise  
- **Overhead** — more layers than simple collections  
- **Enterprise focus** — best suited for large, maintainable test suites  

---

## When to Use  
✅ Enterprise projects needing **clarity, safety, and maintainability**  
❌ Small prototypes or simple parameterized tests (use `TheoryData`, `TestCaseData`, or manual arrays)

---

In short: **CsabaDu.DynamicTestData.Core** is the backbone of a next‑generation *CsabaDu.DynamicTestData* ecosystem, offering **structure, safety, and traceability** far beyond built‑in framework types.

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
