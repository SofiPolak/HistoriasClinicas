# Feedback 📢

# Table of Contents
1. [Entrega 1](#entrega-1)

## entrega-1

Los constructores fueron removidos ya que como dijimos en clase no tienen razón de ser en estos casos donde usamos modelos anémicos. Nuestros modelos simplemente se encargan de tener las propiedades que contienen la información de cada tipo.

### Clase Usuario

- La clase se define como abstracta ya que no existirán usuarios como tales, sino que siempre los usuarios que accedan al sistema serán o bien `Empleado` o bien `Medico` o bien `Paciente`.
- Todavía no se vio el tema en clase, pero la propiedad `Password` la ponemos como tipo array de bytes para almacenarla encriptada.
- Se agrega la propiedad `Apellido` ya que será utilizada en todas las clases derivadas.
- Se agrega la propiedad `Dni` ya que será utilizada en todas las clases derivadas. Adicionalmente la implementamos como string para poder aplicarle el formato que queramos en la entrada (por ejemplo con un validador de tipo RegularExpression como vimos en clase).
- Se agrega la propiedad `Telefono` ya que será utilizada en todas las clases derivadas. Adicionalmente la implementamos como string para poder aplicarle el formato que queramos en la entrada (por ejemplo con un validador de tipo RegularExpression como vimos en clase).
- Se agrega la propiedad `Direccion` ya que será utilizada en todas las clases derivadas. Adicionalmente la utilizamos como string para simplificar el ejercicio en este punto.

### Clase Paciente

- Hereda de la clase `Usuario` y ya que hereda de dicha clase, eliminamos todas las propiedades que se "heredan".

### Clase Medico

- Hereda de la clase `Usuario` y ya que hereda de dicha clase, eliminamos todas las propiedades que se "heredan".

### Clase Empleado

- Hereda de la clase `Usuario` y ya que hereda de dicha clase, eliminamos todas las propiedades que se "heredan".

### Clase Direccion

- Si bien está perfecto que hayan creado la clase dirección, para simplificarles el ejercicio al menos un poco prefiero que las direcciones las traten como un string simplemente.

### Clase HistoriaClinica

- Se agregó la property `Id` que faltaba.

