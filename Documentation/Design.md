# Design/Architektur

Unten stehende Skizze zeigt die Architektur der Anwendung. Ziel war dabei die Trennung von Front-End und Back-End, sowie die Aufteilung von Zuständigkeiten in der Logik in separate Klassen.

* Um möglichst flexibel zu bleiben und die konkrete Implementierung austauschbar zu halten, wird die Funktionalität in einer Schnittstelle `Service-Interfaces` abstrahiert. 
* Innerhalb des Front-Ends wird auf eine MVVM-Architektur mit XAML/WPF gesetzt, um ggf. schnell zu einer Weboberfläche wechseln zu können, ohne die Anbindung an die Service-Interfaces überarbeiten zu müssen. 
* Der `Service` stellt die eigentliche Logik, das Backend dar, auf den das Front-End über die Schnittstellen zugreifen kann. Der Zugriff auf die Daten wird in diesem Prototyp nur angedeutet und von den Service-Klassen selbst implementiert. Über die `Repository-Interfaces` kann aber auch hier später schnell auf die echte Datenanbindung gewechselt werden.

![Architektur](https://github.com/IngmarBuchenhain/QualityContacts/blob/d5d2217c59a0f4d476ff2e758096aec0fee1e3d5/Documentation/Images/Hierarchische%20Software%20Architektur.png)


