# Fundamental concepts in programming

## Interface

An **interface** is a general term which represents how one can interact with a **system**, or how different systems can interact with each other.

### Example: computer system

<!-- diagram here -->

For example, take computers.
The computer, which represents the relevant system, is treated as a magical unknown entity by the user, which is to say, they don't know or care how it works.
They use the keyboard and mouse to send signals to the computer, and look at the computer screen to view responses from the system (e.g. clicking the mouse button opens a program, typing on the keyboard types the keypresses into the text editor software).
These things represent the interface of a computer system.
The user is completely oblivious to the inner workings of the computer system, they might as well not know it exists, because all they ever interact with is the interface.

> If you find not knowing it exists unbelievable, ask youself if you know what exact parts your computer is made up of,
> or could you tell by looking at the interface if some of those parts were swapped out.
> You probably don't, which goes to show that your computer is likely just a "black box" for you.

To be clear, the computer system is never considered a part of the interface, the interface is comprised of just the input and the output devices that help the user interact with the computer system.
Anything that's used to interact with the computer is considered part of the interface, even USB ports on the computer box, or the power switch.

In general terms, the user of a system that does actions is often called an **actor**, while the system in question is often referred to as an **implementation**, especially in the programming sense of the term.

These terms are often used in the design of software systems.

### Example: video game

<!-- diagram here -->

Let's take another example -- a video game.
It's common and good practice to separate the game components into the underlying game logic part, the user input part and the graphics, or the view part.
The **view** or the graphics part is what the user would see on the screen, aka the character animations, buttons, damage numbers, etc.
The user input part, often named **controller**, is the part of the system responsible for converting user input, like clicking buttons on the screen or keys on the keyboard, into commands that can be interpreted by the game logic, like moving the character, or zooming the camera, or using an ability.
The game logic part, also named **model**, applies the user input from the controller and displays the results via the view to the user.

Now, this setup would interact with the user through the view (showing the game state to the user), but also by interpreting commands from the input devices, via the controller.
So it's not wrong to say that the controller is also part of the interface.


### Breaking the system down into individual parts

The difference between an interface and a **user interface** is that the latter is designed specifically for interaction with humans, while interface is a more general term. It's possible for one interface to interact with another.

For example, a keyboard has on its own a interface.
A keyboard has an input part (the keys), the implementation or the **"black box"** system part (how key presses are converted into electric waves) and the output part (the wire that connects to the computer).
The input and the output parts comprise the interface of a keyboard.
The input is manipulated by humans, while the output is interpreted by the computer.
An observation is that in an interface, the actor is not necessarily the **observer** of the effects. In case of computers, the effect eventually reaches the actor via the graphical interface, but in scope of a keyboard it's not necessarily the case.

Likewise, a computer screen has its own interface.
It's got the actual screen that's seen by the user, it's got the input wire that receives signals from the computer, and it's got some system that knows which LEDs to light up.
In this case, the actor of this system is the computer, while the observer is the person looking at the screen.

Any part of e.g. the computer is its own little system and has its own little interface.
This is done this way so that any part could be substituted for any other compatible part, making the entire system highly **modular** and flexible. For example, you can connect any keyboard or any screen to a computer, as long as it has a kind of wire that the *output* (screen) or *input* (keyboard) *interface* of a computer supports.

So interfaces allow part designers to **implement** the parts in isolation, as long as these parts comply to the interface specification that the part **consumers** (the interfaces that will allow connection to these parts / use them) require, consituting the interface of these parts.
In other words, interfaces are the outward facing components of things, which are able to connect to other things via their interfaces, because their inputs correspond to the other's outputs, or vice-versa (they match each other's expections).


## Abstraction

Here I will introduce the two very important concepts of **abstraction** and **the level of abstraction**.

**Abstraction** refers to the idea that actors interact with the system via its interface, without caring about the implementation details.
For example, the keyboard is an abstraction for inputting text, because to input text the user would just use the interface (keyboard) that's designed for that purpose, without worrying about how their key presses would get converted into text on the screen (implementation details).

The **level of abstraction** refers to the idea of how small the part that you interact with is.
The more individual smaller parts a thing is made up of, the higher its level.
In our computer example, interacting with the computer system as a whole, using a keyboard and looking at the screen, is a high level interaction.
The interaction of a keyboard and the computer is a slightly lower level interaction, because the keyboard and the interface of the computer that supports keyboard input is a smaller part of the entire computer system.
The smaller the part is, the lower the level. For example, a key on the keyboard would be a very low level interaction, because it essentially just physically presses the button.
The lowest level interaction on a computer would be sending an electric wave through a wire, or flipping a physical bit from 0 to 1.

### How does this relate to programming?

Programmers abstract processes and algorithms.
Code is the essential tool that allows us programmers to create said abstractions.
When broken down, these abstractions come down to sequences of simple fundamental operations, like addition of two numbers or writing a number into some location in memory.

Abstraction is ofter considered a synonym for "simplification" by programmers.
The idea is that abstraction helps simplify interactions with a system by offering a simple interface, which allows the user to not care about the implementation details of the system.


## File system and paths

[See](https://docs.oracle.com/cd/E19504-01/802-5817/6i9i42q3j/index.html) if you don't know what this is.
It is required in order to properly understand the CLI.

## Compilation

Read e.g. [this](https://www.computerhope.com/jargon/c/compile.htm) if you don't understand compilation at a high level.

## Command Line

[General information about CLIs](https://www.lifewire.com/what-is-a-command-line-interpreter-2625827)

A command line interface is an interface that allows launching or executing commands (or programs) and communicating with them.
On Windows, the most commonly used, simple CLI application (program) is the CMD (short for "command").

> To open CMD, you can hit the ⊞ Win button to pop open the search bar, type "cmd" to search for the CLI program and hit enter to run it.

Commands (and programs) are run by typing their name (or path) and hitting Enter.
Commands are directed at the CLI system (clear the screen, change the current working directory, etc.).
Programs are run by the operating system.
After starting a program, and until it completes, whatever you type into the CLI application can potentially be read by the program.

Like mentioned above, to start a program, you type its name and hit Enter.
For example, if there's a file named "prog.exe" and you type "prog.exe" and hit Enter, it will run the file "prog.exe".
".exe" is optional to type on Windows, so just typing "prog" would also start the same program.
".exe" is the only special extension that works like this, to run programs with other extensions you need to type the name fully.

> "exe" stands for "executable".

CLI applications have the concept of the CWD (current working directory), which is very important to know for all programmers.
The current working directory refers to the folder that the program names will be resolved relative to.
So to start the file named "prog.exe", the working directory needs to be the same with the file that you want to run.
Otherwise, it won't be able to find the file you're referring to*.
The CWD path can be used by the program in order to find out where it's being run from.

You can change the working directory by using the `cd` command.
- To navigate to a subfolder named "A", you do `cd A`.
- To return back to the parent directory, you do `cd ..`, double dot meaning "go back".
- Finally, if you want to change the working directory to some **absolute path**, like `C:/Users/Anton/Documents/projects` or `D:/Downloads/movies`, you just type `cd` followed by that absolute path.
- If you're trying to switch e.g. from disk `C` to disk `D`, you also need to input the disk name you're trying to switch to as a command `D:` and hit Enter.

The last concept related to CLIs are the environment variables.
These might sound scary, but they are just some named strings of characters that can be used by the CLI or the programs that you run from it.
The most important is the `PATH` variable, which contains the folders into which the operating system should look to resolve a program name.

For example, say the CWD is `D:/Downloads`, but your program is in `C:/Programs` and is named `prog.exe`.
You could go to the `C:/Programs` directory and run `prog.exe`, or you can run the program by typing its absolute path, `C:/Programs/prog.exe`, but it's not as convenient as just typing `prog.exe` and having it just work.
That's what `PATH` is for.
If you set `PATH` to `C:/Programs`, then you'll be able to run `prog.exe` from anywhere by typing its file name.
The operating system will try to find `prog.exe` in the current directory, and if it can't, it will try and look into the directory specified in the `PATH` variable, aka the path `C:/Programs`, where it will find you program.

`PATH` can be set to a *";"-delimited list of paths*, which means it can include multiple such directory paths in which it should look for programs, you just have to separated them with ";".
For example, `C:/Folder1;C:/Folder2`. When you try to run a program, it will try and look into all of these in order, until it finds the program.


## Processor and Memory

[Processors](https://www.makeuseof.com/tag/cpu-technology-explained/), read the first 2 paragraphs.

[RAM](https://www.makeuseof.com/tag/quick-dirty-guide-ram-need-know/), read the first paragraph.