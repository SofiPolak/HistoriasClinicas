# Historias Clínicas 📖

## Objetivos 📋

Desarrollar un sistema de historias clínicas para un consultorio que permita la administración y gestión de las mismas.\
Los empleados tendrán gestión sobre pacientes, médicos, empleados, historias clínicas, episodios, evoluciones, epicrisis, diagnósticos, etc.\
Los pacientes podrán realizar consultas acerca de su historia clínica.\
Utilizar Visual Studio 2019 y crear una aplicación utilizando `ASP.NET MVC Core versión 3.1`.

---------------------------------------

## Enunciado 📢

La idea principal de este trabajo práctico, es que ustedes se comporten como un equipo de desarrollo. Este documento, les acerca un equivalente al resultado de una primera entrevista entre el cliente y alguien del equipo que relevó e identificó la información aquí contenida. A partir de este momento, deberán comprender lo que se está requiriendo y construir dicha aplicación.\
Deben recopilar todas las dudas que tengan y evacuarlas con su nexo (el docente) de cara al cliente. De esta manera, él nos ayudará a conseguir la información ya un poco más procesada. Es importante destacar que este proceso no debe esperar a ser en clase, sino que debe darse a medida que vayan trabajando en el proyecto. Por otro lado es importante que agrupen sus consultas ya sea por criterios funcionales o técnicos y envíen correos con las consultas agrupadas en lugar de enviar cada consulta de forma independiente.

### Consultas

Las consultas que sean realizadas por correo a mailto:federico.marchese@ort.edu.ar deben seguir el siguiente formato:

**Subject:**

- `[NT1-<CURSO LETRA>-GRP-<GRUPO NUMERO>] <Proyecto XXX> | Informativo o Consulta` *ej: [NT1-A-GRP-5] Agenda de Turnos | Consulta*

**Body:**

1. `<xxxxxxxx>` *ej: ¿La relación del paciente con Turno es 1:1 o 1:N?*
2. `<xxxxxxxx>` *ej: ¿Está bien que encaremos la validación del turno activo, con una propiedad booleana en el Turno?*

---------------------------------------

## Proceso de ejecución en alto nivel ☑️

- Crear un proyecto utilizando [visual studio].
- Adicionar todos los modelos dentro de la carpeta Models cada uno en un archivo separado.
- Especificar todas las restricciones y validaciones solicitadas a cada una de las entidades. [DataAnnotations].
- Crear las relaciones entre las entidades.
- Crear una carpeta Data que dentro tendrá al menos la clase que representará el contexto de la base de datos DbContext.
- Crear el DbContext utilizando base de datos sqlite. [DbContext], [Database Sqlite], [Db browser sqlite].
- Agregar los DbSet para cada una de las entidades en el DbContext.
- Crear el Scaffolding para permitir los CRUD de las entidades.
- Aplicar las adecuaciones y validaciones necesarias en los controladores.
- Realizar un sistema de login para los roles identificados en el sistema y un administrador.
- Un administrador podrá realizar todas tareas que impliquen interacción del lado del negocio (ABM "Alta-Baja-Modificación" de las entidades del sistema y configuraciones en caso de ser necesarias).
- Cada usuario sólo podrá tomar acción en el sistema en base al rol que tiene.
- Realizar todos los ajustes necesarios en los modelos y/o funcionalidades.
- Realizar los ajustes requeridos del lado de los permisos.
- Todo lo referido a la presentación de la aplicaión (cuestiones visuales).
- Para la visualización se recomienda utilizar [Bootstrap], pero se puede utilizar cualquier herramienta que el grupo considere.

---------------------------------------

## Entidades básicas 📄

- Usuario
- Paciente
- Médico
- Empleado
- Historia clínica
- Episodio
- Evolución
- Notas
- Epicrisis
- Diagnóstico

`Importante: Todas las entidades deben tener su identificador único Id de tipo Guid.`

`Las propiedades descriptas a continuación, son las mínimas que deben tener las entidades, dejando explícito que ustedes pueden agregar las que consideren necesarias.  
De la misma manera deben definir los tipos de datos asociados a cada una de ellas, como así también las restricciones que apliquen.`

| Entidad | Propiedades |
| ----- | ----- |
| Usuario | Nombre, Email, FechaAlta, Password |
| Paciente | Nombre, Apellido, DNI, Telefono, Direccion, FechaAlta, Email , ObraSocial, HistoriaClinica |
| Medico | Nombre, Apellido, DNI, Telefono, Direccion, FechaAlta, Email, Matricula, Especialidad |
| Empleado | Nombre, Apellido, DNI, Telefono, Direccion, FechaAlta, Email, Legajo |
| HistoriaClinica | Paciente, Episodios |
| Episodio | Motivo, Descripcion, FechaYHoraInicio, FechaYHoraAlta, FechaYHoraCierre, EstadoAbierto, Evoluciones, Epicrisis, EmpleadoRegistra |
| Evolucion | Medico, FechaYHoraInicio, FechaYHoraAlta, FechaYHoraCierre, DescripcionAtencion, EstadoAbierto, Notas |
| Nota | Evolucion, Empleado, Mensaje, FechaYHora |
| Epicrisis | Episodio, Medico, FechaYHora, Diagnostico |
| Diagnostico | Epicrisis, Descripcion, Recomendacion |

**NOTA:** aquí un link para refrescar el uso de los [Data annotations].

---------------------------------------

## Características y Funcionalidades ⌨️

`Todas las entidades deben tener implementado su correspondiente ABM, a menos que sea implícito el no tener que soportar alguna de estas acciones.`

`IMPORTANTE: Ninguna entidad en el circuito de atención médica puede ser modificado o eliminado una vez que se ha creado. Ej. No se puede Eliminar una historia clínica, no se puede modificar una nota de una evolución, etc.`

### Paciente

- Los Pacientes pueden auto registrarse.
  - La autoregistración desde el sitio es exclusiva para los pacientes, por lo cual se le asignará dicho rol automáticamente.
- Un paciente puede consultar su historia clínica con todos los detalles que la componen.
- Un paciente puede acceder a los episodios y, por cada episodio, ver las evoluciones con sus detalles.
- Un paciente puede actualizar sus datos de contacto tales como el teléfono, dirección,etc., pero no puede modificar sus datos personales tales como el Dni, nombre, apellido, etc.

### Empleado

- Los empleados deben ser agregados por otro empleado.
  - Al momento del alta del empleado se le definirá un username y password y se le asignará el rol de empleado.
- Un empleado, puede modificar todos los datos de los pacientes.
  - No puede quitar o asociar una nueva historia clínica a los pacientes.
- El Empleado puede listar todos los pacientes y, por cada uno, ver detalles como las historias clínicas que tiene asociada y si tiene episodios abiertos.
- El Empleado puede dar de alta pacientes, empleados y médicos, cada uno de ellos con su correspondientes datos requeridos y rol.
- El Empleado puede crear un episodio para un paciente en su historia clínica con su correspondiente motivo y descripción, pero sólo se limita a dicha creación.

### Médico

- Los médicos deben ser agregados por un empleado.
  - Al momento del alta del médico se le definirá un username y password y se le asignará el rol de médico.
- Un médico puede crear evoluciones en Episodios que esten en estado abierto.
  - Para ello buscará al paciente, accederá a su `historia clínica -> episodio -> crear la evolución`.
- Un médico puede cerrar una evlución si se han completado todos los campos. El campo de FechaYHoraCierre, se guardará automáticamente.
  - Un médico, pueden cargar notas en cada evolución según sea necesario.
  - Las notas pueden continuar agregándose luego del cierre de la evolución.
- Un médico puede cerrar un episodio, pero para hacer esto, el sistema realizará ciertas validaciones.

### Historia clínica

- La misma se crea automáticamente con la creación de un paciente.
  - No se puede eliminar, ni realizar modificaciones posteriores.
  - El detalle interno de la misma es para los médicos y empleados.
  - El paciente propietario de la historia clínica es el unico paciente que puede ver su historia clínica.
- Por medio de la historia clínica se podrá acceder a la lista de episodios relacionados.

### Episodio

- La creación de un episodio en una historia clínica solamente puede ser realizada por un empleado.
  - El empleado deberá acceder a un `paciente -> historia clínica -> crear episodio` e ingresará:
    - **Motivo**. *Ej. Traumatismo en pierna Izquierda.*
    - **Descripción**. *Ej. El paciente se encontraba andando en Skate y sufrió un accidente.*
- El episodio se:
  - Creará en estado *Abierto* automáticamente.
  - Con una FechaYHoraInicio también de forma automática.
  - Con un empleado asociado, o sea, el *registrador* de dicho episodio (persona en recepción que recibe al paciente).
- Sólo un médico puede cerrar un Episodio. Para hacer esto, el sistema validará:
  1. Que el episodio no tenga ninguna evolución en estado *Abierta* o no tenga evoluciones. Si fuese así, deberá mostrar un mensaje (ver nota debajo para el caso sin evoluciones).
  2. Cargará el médico manualmente la FechaYHoraAlta (alta del episodio) del paciente.
  3. Le pedirá que cargue una epicrisis con su diagnóstico y recomendaciones.
      - Una vez finalizado el diagnóstico, el episodio pasará a estar en estado *Cerrado*.
  4. La FechaYHoraCierre será cargada automáticamente si se cumplen los requerimientos previos.
**Nota**: Si el cierre del episodio es por la condición `sin evoluciones`, se generará un "Cierre Administrativo" en el cual, el sistema cargará una epicrisis, con alguna información que el empleado ingresará para dejar registro de que fue un cierre administrativo. *Ej. El paciente realizó el ingreso y antes de ser atendido, se fué.*

### Evolución

- Una evolución solo la puede crear y gestionar un médico.
  - La única excepción es que un empleado puede cargar notas en evoluciones por cuestiones administrativas. *Ej. Salvo que el alta del paciente en la evolución es 10/08/2020*
- Información que se cargará automáticamente en las evoluciones:
  - En el alta de una evolución:
    - El médico que la está creando.
    - La fecha y hora de inicio de la evolución.
    - El estado *Abierto*.
  - En el cierre de una evolución:
    - La fecha y hora de cierre de la evolución.
- Información que se cargará manualmente en las evoluciones:
  - La fecha y hora de alta de la evolución.
  - La descripción.
  - Las notas (Las que sean necesarias).
- Para cerrar una evolución:
  - Todos los datos manuales requeridos deben estar completos
  - Sólo lo puede hacer un médico.

### Nota

- Las notas se crean a partir de una evolución.
- A través de las notas pueden cargar un mensaje cualquier empleado o médico.
- Quedará automáticmente registrado el autor de la nota y la fecha y hora de carga.

### Epicrisis

- La epicrisis pertene a un episodio.
  - Sólo puede haber una epicrisis por episodio.
  - Para poder crearla todas las evoluciones deben estar cerradas.
  - El episodio debe estar abierto y cargar la epicrisis, de estar todo correcto, se debe cerrar automáticamente.
  - La epicrisis sólo puede ser cargada por un médico.
  - La excepción es la creación automática, si cierra un empleado, por proceso administrativo (recordar que un empleado puede cerrar un episodio sin evoluciones).
  - La fecha y hora se carga automáticamente.
  - El diagnóstico se carga manualmente.

### Diagnóstico

- Pertenece a una epicrisis.
- Se cargará una descripción y una recomendación de forma manual.

### Aplicación General

- Información institucional.
- Se deben listar el cuerpo médico junto con sus especialidades.
- Los accesos a las funcionalidades y/o capacidades, debe estar basada en los roles que tenga cada individuo.

[//]: # (referencias externas)
   [visual studio]: <https://visualstudio.microsoft.com/en/vs/>
   [Data annotations]: <https://www.c-sharpcorner.com/UploadFile/af66b7/data-annotations-for-mvc/>
   [Bootstrap]: <https://getbootstrap.com/>
   [DbContext]: <https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontext?view=efcore-3.1>
   [Database Sqlite]: <https://docs.microsoft.com/en-us/ef/core/providers/sqlite/?tabs=dotnet-core-cli>
   [Db browser sqlite]: <https://sqlitebrowser.org/>
   [DataAnnotations]: <https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=netcore-3.1>
