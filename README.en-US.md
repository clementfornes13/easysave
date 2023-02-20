<a name="readme-top"></a>

[![en-US](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/eystone/prosoft/blob/solution/README.en-US.md)
[![fr-FR](https://img.shields.io/badge/lang-fr-blue.svg)](https://github.com/eystone/prosoft/blob/solution/README.md)

<!-- LOGO PROJET -->
<br />
<div align="center">
  <a href="https://github.com/eystone/prosoft">
    <img src="https://github.com/eystone/prosoft/blob/main/EasySave/Images/easysave logo.svg" alt="Logo" width="150" height="150">
  </a>

  <h3 align="center">EasySave</h3>

  <p align="center">
    Saving is now easy, with EasySave !
    <br />
  </p>
</div>


<!-- Table of contents -->
<details>
  <summary>Table of contents</summary>
  <ol>
    <li>
      <a href="#About the project">About the project</a>
    </li>
    <li>
      <a href="#Getting started">Getting started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <lia><a href="#versionhistory">Version history</a></li>
    <li><a href="#resources">Resources</a></li>
  </ol>
</details>


<!-- About the project -->
## About the project

![Project screen][product-screenshot]
![Project screen2][product-screenshot2]

EasySave is an application that has a visual interface as well as a console that allows users to create, delete and execute saved files. 
The program supports English and French languages.

<p align="right">(<a href="#readme-top">Back to top</a>)</p>


<!-- Getting started -->
## Getting started

### Prerequisites

    Windows
    .NET Framework installed
    Json.NET Framework installed

* Json.NET
  ```sh
  dotnet add package Newtonsoft.Json --version 13.0.2
  ```


### Installation

1. Clone the repo
   ```sh
   git clone https://github.com/eystone/prosoft.git
   ```
2. Install Json.NET Framework
   ```sh
   dotnet add package Newtonsoft.Json --version 13.0.2
   ```

<p align="right">(<a href="#readme-top">Back to top</a>)</p>


<!-- Examples -->
## Usage

Add examples

<p align="right">(<a href="#readme-top">Back to top</a>)</p>


<!-- ROADMAP -->
## Roadmap

- [x] Save file option
- [x] Logs
- [x] Delete file option
- [x] Execute file option
- [x] Multi-language support
    - [x] French
    - [x] English
- [x] WPF Application
- [ ] Remote interface

<p align="right">(<a href="#readme-top">Back to top</a>)</p>


<!-- version history -->
## Version history

- Version 1 : 
	> Multi-Language Console App

	> Log, State Files in JSON Format

	> Limited Saves (5)

	> Sequential save type

- Version 1.1 : 
	> Adding log file in XML Format
- Version 2 
	> WPF Application

	> Unlimited Saves
	
    > Adding CryptoSoft use
	
    > Adding encryption time in log file
	
    > Work application detection (Can't launch save)
- Version 3 :
	> Adding pause option

	> Differential or Sequential save type
	
    > Work application detection (Stop all in progress saves)
	
    > Prioritary files management

<p align="right">(<a href="#readme-top">Revenir en haut</a>)</p>


<!-- Resources -->
## Resources

* [Json.NET Documentation](https://www.newtonsoft.com/json/help/html/Introduction.htm)
* [Tutoriels Visual Studio | C#](https://learn.microsoft.com/fr-fr/visualstudio/get-started/csharp/?view=vs-2022)

<p align="right">(<a href="#readme-top">Back to top</a>)</p>


<!-- Lien et images -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[product-screenshot]: https://github.com/eystone/prosoft/blob/main/EasySave/Images/capture.en-US.PNG
[product-screenshot2]: https://i.imgur.com/8QC76bn.png
