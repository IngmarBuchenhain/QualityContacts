# Design/Architektur

Um möglichst flexibel zu bleiben und die konkrete Implementierung austauschbar zu halten, wird die Funktionalität in einer Schnittstelle „Service-Interfaces“ abstrahiert. Innerhalb des Front-Ends wurde auf eine MVVM-Architektur gesetzt, um ggf. schnell zu einer Weboberfläche wechseln zu können, ohne die Anbindung an die Service-Interfaces überarbeiten zu müssen. Der „Service“ stellt die eigentliche Logik, das Backend dar, auf den das Front-End über die Schnittstellen zugreifen kann. Der Zugriff auf die Daten wird in diesem Prototyp nur angedeutet und von den Service-Klassen selbst implementiert.

![Architektur](https://github.com/IngmarBuchenhain/QualityContacts/blob/d5d2217c59a0f4d476ff2e758096aec0fee1e3d5/Documentation/Images/Hierarchische%20Software%20Architektur.png)


