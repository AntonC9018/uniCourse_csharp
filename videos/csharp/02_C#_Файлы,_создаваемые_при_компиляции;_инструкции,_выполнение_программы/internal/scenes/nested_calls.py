"""Nested calls and both returns, matching the note's reduced sample.

Recording coverage: 579–637 seconds. Other, A and B are illustrative, as
qualified in the note. Holds support reading; they do not represent CPU time.
"""

from manim import (
    Scene, Text, VGroup, RoundedRectangle, Line, Polygon, Dot, VMobject,
    FadeIn, MoveAlongPath, LEFT, linear,
)


class CourseAnimation(Scene):
    """Trace startup -> Hello -> Other -> Hello -> startup -> program end."""

    def construct(self):
        self.camera.background_color = "#101827"
        ink = "#EEF3FC"
        muted = "#AABBD2"
        active = "#F6C85F"
        done = "#75DEB3"
        saved = "#83C9F4"
        border = "#40536E"
        mono = "DejaVu Sans Mono"
        sans = "DejaVu Sans"

        def label(value, size=22, color=ink, font=sans):
            return Text(value, font=font, font_size=size, color=color)

        def at_left(value, x, y, size=22, color=ink, font=sans):
            return label(value, size, color, font).move_to(
                [x, y, 0], aligned_edge=LEFT,
            )

        def panel(width, height, x, y):
            return RoundedRectangle(
                width=width, height=height, corner_radius=0.12,
                stroke_color=border, stroke_width=1.5,
                fill_color="#182438", fill_opacity=1,
            ).move_to([x, y, 0])

        title = label("Calls inside function bodies", 32).move_to([0, 3.49, 0])
        subtitle = label(
            "Reduced sample • Other, A and B are illustrative", 21, muted,
        ).move_to([0, 2.96, 0])
        panels = VGroup(
            panel(8.0, 5.5, -2.6, -0.10),
            panel(4.7, 3.26, 4.1, 1.02),
            panel(4.7, 2.06, 4.1, -1.82),
        )
        headings = VGroup(
            at_left("Program.cs", -6.17, 2.28, 23),
            at_left("Call stack", 2.04, 2.28, 23),
            at_left("Console output", 2.04, -1.06, 22),
        )
        rules = VGroup(
            Line([-6.17, 2.02, 0], [1.02, 2.02, 0], color=border),
            Line([2.04, 2.02, 0], [6.16, 2.02, 0], color=border),
        )

        statements = [
            "Hello();",
            "void Hello()",
            "{",
            "Console.WriteLine(1);",
            "Console.WriteLine(2);",
            "Console.WriteLine(3);",
            "Other();",
            "}",
            "void Other()",
            "{",
            'Console.WriteLine("A");',
            'Console.WriteLine("B");',
            "}",
        ]
        line_numbers = [1, 3, 4, 5, 6, 7, 8, 9, 11, 12, 13, 14, 15]
        row_y = [
            1.70, 0.94, 0.64, 0.34, 0.04, -0.26, -0.56,
            -0.86, -1.38, -1.68, -1.98, -2.28, -2.58,
        ]
        rows = VGroup()
        numbers = VGroup()
        for index, (statement, number, y) in enumerate(
            zip(statements, line_numbers, row_y)
        ):
            x = -5.11 if index in (3, 4, 5, 6, 10, 11) else -5.61
            rows.add(at_left(statement, x, y, 20, ink, mono))
            numbers.add(label(str(number), 18, muted, mono).move_to([-6.02, y, 0]))

        startup_tag = at_left("Startup", -1.11, 1.70, 19, muted)
        definitions_tag = at_left("Definitions", -1.11, 1.18, 18, muted)
        pointer = Polygon(
            [-6.45, 0.09, 0], [-6.28, 0, 0], [-6.45, -0.09, 0],
            stroke_width=0, fill_color=active, fill_opacity=1,
        )

        # Stack order is top first: the current body is above its waiting caller.
        stack = VGroup()
        stack_empty = at_left("No active calls yet", 2.10, 1.58, 20, muted)
        output_empty = at_left("No output yet", 2.10, -1.46, 20, muted)
        output_rows = [
            at_left(value, 2.12, -1.43 - 0.29 * i, 22, ink, mono)
            for i, value in enumerate(["1", "2", "3", "A", "B"])
        ]

        status = label("Start at the startup call", 25).move_to([0, -3.18, 0])
        detail = label(
            "Definitions supply bodies. Calls choose when those bodies run.", 20, muted,
        ).move_to([0, -3.65, 0])

        def set_state(primary, secondary, color=ink):
            nonlocal status, detail
            self.remove(status, detail)
            status = label(primary, 25, color).move_to([0, -3.18, 0])
            detail = label(secondary, 20, muted).move_to([0, -3.65, 0])
            self.add(status, detail)

        def set_stack(frames):
            nonlocal stack
            self.remove(stack, stack_empty)
            stack = VGroup()
            for i, (name, continuation, color) in enumerate(frames):
                y = 1.55 - 0.88 * i
                box = RoundedRectangle(
                    width=4.10, height=0.75, corner_radius=0.08,
                    stroke_color=color, stroke_width=1.8,
                    fill_color="#111E2D", fill_opacity=1,
                ).move_to([4.10, y, 0])
                stack.add(
                    box,
                    at_left(name, 2.22, y + 0.16, 20, color),
                    at_left(continuation, 2.22, y - 0.17, 18, muted),
                )
            self.add(stack)

        def activate(index):
            pointer.move_to([-6.36, row_y[index], 0])
            rows[index].set_color(active)
            numbers[index].set_color(active)
            self.add(pointer)

        def complete(index):
            rows[index].set_color(done)
            numbers[index].set_color(done)
            self.remove(pointer)

        def transfer(start, end, color):
            # Travel through the gutter, never through intervening definitions.
            self.remove(pointer)
            path = VMobject().set_points_as_corners([
                [-6.36, row_y[start], 0], [-6.82, row_y[start], 0],
                [-6.82, row_y[end], 0], [-6.36, row_y[end], 0],
            ]).set_stroke(color, width=2)
            token = Dot(path.get_start(), radius=0.055, color=color)
            self.add(path, token)
            self.play(MoveAlongPath(token, path), run_time=1.5, rate_func=linear)
            self.remove(path, token)

        def print_value(index, output_index, body, value):
            activate(index)
            set_state(
                f"{body}: print {value}",
                "Finish this statement before moving to the next one.", active,
            )
            self.wait(0.8)
            self.remove(output_empty)
            self.play(FadeIn(output_rows[output_index]), run_time=0.3)
            self.wait(1.2)
            complete(index)
            self.wait(0.7)

        startup_waiting = ("Startup • waiting", "Hello() must finish", saved)
        hello_waiting = ("Hello • waiting", "Other() must finish", saved)
        self.add(
            title, subtitle, panels, headings, rules, rows, numbers,
            startup_tag, definitions_tag, stack_empty, output_empty, status, detail,
        )
        self.wait(3)

        activate(0)
        set_stack([startup_waiting])
        set_state(
            "Startup calls Hello",
            "Save startup's continuation: after Hello(), there is no next statement.",
            active,
        )
        self.wait(3)

        set_state(
            "Enter Hello at its first statement",
            "The startup call stays pending while Hello runs.", active,
        )
        transfer(0, 3, active)
        set_stack([("Hello • running", "Print 1, then 2, then 3", active), startup_waiting])
        activate(3)
        self.wait(1.5)

        for index, value in enumerate(["1", "2", "3"]):
            print_value(3 + index, index, "Hello", value)

        activate(6)
        set_stack([hello_waiting, startup_waiting])
        set_state(
            "Hello's final statement calls Other",
            "Hello cannot finish yet: its Other() call is still pending.", active,
        )
        self.wait(3)

        set_state(
            "Enter Other at its first statement",
            "Both callers wait. Other now runs its own body.", active,
        )
        transfer(6, 10, active)
        set_stack([
            ("Other • running", "Print A, then B", active),
            hello_waiting, startup_waiting,
        ])
        activate(10)
        self.wait(1.5)

        print_value(10, 3, "Other", "A")
        print_value(11, 4, "Other", "B")

        activate(12)
        set_stack([
            ("Other • finished", "Return to Hello's Other()", done),
            hello_waiting, startup_waiting,
        ])
        set_state(
            "Other's body is finished",
            "Return 1 goes to the Other() call inside Hello.", done,
        )
        self.wait(3)
        complete(12)

        set_state(
            "Return 1: Other → Hello",
            "Resume the caller at line 8. The nested call can now complete.", saved,
        )
        transfer(12, 6, saved)
        activate(6)
        set_stack([
            ("Hello • resumed", "Other() has completed", done), startup_waiting,
        ])
        complete(6)
        set_state(
            "Back in Hello: Other(); is completed",
            "This was Hello's last statement. Startup is still waiting.", done,
        )
        self.wait(4)

        activate(7)
        set_stack([
            ("Hello • finished", "Return to startup's Hello()", done), startup_waiting,
        ])
        set_state(
            "Hello now finishes",
            "Its final nested call has completed; there are no body statements left.", done,
        )
        self.wait(4)
        complete(7)

        set_state(
            "Return 2: Hello → startup",
            "Return to the original Hello() call at line 1.", saved,
        )
        transfer(7, 0, saved)
        activate(0)
        set_stack([("Startup • resumed", "Hello() has completed", done)])
        complete(0)
        set_state(
            "Back in startup: Hello(); is completed",
            "Both returns have happened: Other → Hello, then Hello → startup.", done,
        )
        self.wait(4)

        set_state(
            "No startup statements remain",
            "The nearby definitions do not run as extra startup actions.", done,
        )
        self.wait(4)
        set_stack([])
        ended = at_left("Program ended", 2.10, 1.58, 22, done)
        self.add(ended)
        set_state(
            "Program ends • Output: 1, 2, 3, A, B",
            "Each call finished before its caller could finish.", done,
        )
        self.wait(6)
