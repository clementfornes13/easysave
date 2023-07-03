<a name="readme-top"></a>

[![fr-FR](https://img.shields.io/badge/lang-fr-blue.svg)](https://github.com/eystone/prosoft/blob/solution/README.md)
[![en-US](https://img.shields.io/badge/lang-en-red.svg)](https://github.com/eystone/prosoft/blob/solution/README.en-US.md)

<!-- LOGO PROJET -->
<br />
<div align="center">
  <a href="https://github.com/eystone/prosoft/solution">
    <img src="https://github.com/eystone/prosoft/blob/main/EasySave/Images/easysave logo.svg" alt="Logo" width="150" height="150">
  </a>

  <h3 align="center">EasySave</h3>

  <p align="center">
    Saving is now easy, with EasySave !
    <br />
  </p>
</div>


<!-- SOMMAIRE -->
<details>
  <summary>Sommaire</summary>
  <ol>
    <li>
      <a href="#a-propos">A propos</a>
    </li>
    <li>
      <a href="#mise-en-place">Mise en place</a>
      <ul>
        <li><a href="#prerequis">Pré-requis</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#utilisation">Utilisation</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <lia><a href="#versionhistory">Historique des versions</a></li>
    <li><a href="#ressources">Ressources utilisés</a></li>
  </ol>
</details>


<!-- A propos -->
## A propos

![Capture d'écran du projet][product-screenshot]
![Capture d'écran du projet2][product-screenshot2]

EasySave est une application qui possède une interface visuelle ainsi que console qui permet aux utilisateurs de créer, supprimer et exécuter des travaux de sauvegarde. 
EasySave est utilisable en Français et en Anglais

<p align="right">(<a href="#readme-top">Revenir en haut</a>)</p>


<!-- Mise en place -->
## Mise en place

### Pré-requis

    Windows
    .NET Framework installé
    Json.NET Framework installé

* Json.NET
  ```sh
  dotnet add package Newtonsoft.Json --version 13.0.2
  ```

### Installation

1. Cloner le dépôt
   ```sh
   git clone https://github.com/eystone/prosoft.git
   ```
2. Installer Json.NET Framework
   ```sh
   dotnet add package Newtonsoft.Json --version 13.0.2
   ```

<p align="right">(<a href="#readme-top">Revenir en haut</a>)</p>


<!-- Exemples -->
## Utilisation

Mettre des exemples par la suite.

<p align="right">(<a href="#readme-top">Revenir en haut</a>)</p>


<!-- ROADMAP -->
## Roadmap

- [x] Possibilité de sauvegarder un travail
- [x] Ajout des logs
- [x] Ajout du suivi des états
- [x] Possibilité de supprimer un travail
- [x] Possibilité d'exécuter un travail
- [x] Support de plusieurs langues
    - [x] Français
    - [x] Anglais
- [x] Application WPF
- [ ] Interface déportée

<p align="right">(<a href="#readme-top">Revenir en haut</a>)</p>


<!-- Historique des versions -->
## Historique des versions

- Version 1 : 
	> Application Console Multi-langues

	> Maximum de 5 travaux de sauvegarde

	> Fichier Log, Etat en JSON

	> Type de fonctionnement (Sequentielle)

- Version 1.1 :
	> Ajout du fichier Log en XML

- Version 2 :
   > Passage en Application WPF
   
   > Travaux de sauvegarde illimités
   
   > Ajout temps de cryptage dans fichier log
   
   > Détection logiciel métier (Pas de lancement de travail)
   
   > Utilisation de CryptoSoft

- Version 3 :
 > Possibilité de mettre en pause

 > Ajout type de fonctionnement (Parallèle)

 > Détection logiciel métier (Arrêt de tout travail en cours)

 > Gestion fichiers prioritaires avant les autres

 > Choix taille maximum de fichier pour sauvegarde en simultané

 > Interface déportée

<p align="right">(<a href="#readme-top">Revenir en haut</a>)</p>


<!-- Ressources utilisées -->
## Ressources

* [Json.NET Documentation](https://www.newtonsoft.com/json/help/html/Introduction.htm)
* [Tutoriels Visual Studio | C#](https://learn.microsoft.com/fr-fr/visualstudio/get-started/csharp/?view=vs-2022)

<p align="right">(<a href="#readme-top">Revenir en haut</a>)</p>


<!-- Lien et images -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[product-screenshot]: https://github.com/eystone/prosoft/blob/main/EasySave/Images/capture.png
[product-screenshot2]: https://i.imgur.com/mt0a6J7.png
