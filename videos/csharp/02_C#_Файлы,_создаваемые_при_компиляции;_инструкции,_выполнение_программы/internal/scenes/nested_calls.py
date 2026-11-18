"""Nested calls and both returns, matching the note's reduced sample.

Recording coverage: 579–637 seconds. Other, A and B are illustrative, as
qualified in the note. Pacing illustrates control flow, not CPU time.
"""

from manim import (
    Scene, Text, VGroup, RoundedRectangle, Line, Polygon, Dot, VMobject,
    MoveAlongPath, LEFT, linear,
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
            return Text(
                value, font=font, font_size=size, color=color,
                disable_ligatures=True,
            )

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

        title = label("Nested calls", 28).move_to([0, 3.49, 0])
        subtitle = label(
            "Other, A and B are illustrative", 20, muted,
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

        status = label("Startup", 25).move_to([0, -3.32, 0])

        def set_state(value, color=ink):
            nonlocal status
            self.remove(status)
            status = label(value, 25, color).move_to([0, -3.32, 0])
            self.add(status)

        def set_stack(frames):
            nonlocal stack
            self.remove(stack, stack_empty)
            stack = VGroup()
            for i, (name, continuation, color) in enumerate(frames):
                y = 1.55 - 0.84 * i
                box = RoundedRectangle(
                    width=4.10, height=0.72, corner_radius=0.08,
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
            # Control travels in the gutter, past unexecuted definitions.
            self.remove(pointer)
            path = VMobject().set_points_as_corners([
                [-6.36, row_y[start], 0], [-6.82, row_y[start], 0],
                [-6.82, row_y[end], 0], [-6.36, row_y[end], 0],
            ]).set_stroke(color, width=2)
            token = Dot(path.get_start(), radius=0.055, color=color)
            self.add(path, token)
            self.play(MoveAlongPath(token, path), run_time=1, rate_func=linear)
            self.remove(path, token)

        def print_value(index, output_index):
            activate(index)
            set_state("Print", active)
            self.wait(0.4)

            # Copy the literal's glyph to the next console row. The source stays
            # intact. A gutter route avoids all unrelated source and stack text.
            literal = rows[index][-4 if index in (10, 11) else -3]
            target = output_rows[output_index]
            token = literal.copy().set_color(active)
            start = token.get_center()
            end = target.get_center()
            path = VMobject().set_points_as_corners([
                start, [1.55, start[1], 0], [1.55, end[1], 0], end,
            ])
            self.remove(output_empty)
            self.add(token)
            self.play(MoveAlongPath(token, path), run_time=0.65, rate_func=linear)
            self.remove(token)
            self.add(target)
            self.wait(0.55)
            complete(index)
            self.wait(0.1)

        startup_waiting = ("Startup • waiting", "After Hello() → end", saved)
        hello_waiting = ("Hello • waiting", "After Other() → end Hello", saved)
        self.add(
            title, subtitle, panels, headings, rules, rows, numbers,
            startup_tag, definitions_tag, stack_empty, output_empty, status,
        )
        self.wait(1)

        activate(0)
        set_stack([startup_waiting])
        set_state("Call Hello", active)
        self.wait(1.2)
        set_state("Startup → Hello", active)
        transfer(0, 3, active)
        set_stack([
            ("Hello • running", "Return → startup: line 1", active),
            startup_waiting,
        ])
        activate(3)
        self.wait(0.5)

        for index in range(3):
            print_value(3 + index, index)

        activate(6)
        set_stack([hello_waiting, startup_waiting])
        set_state("Call Other • Hello waits", active)
        self.wait(1.4)
        set_state("Hello → Other", active)
        transfer(6, 10, active)
        set_stack([
            ("Other • running", "Return → Hello: line 8", active),
            hello_waiting, startup_waiting,
        ])
        activate(10)
        self.wait(0.5)

        print_value(10, 3)
        print_value(11, 4)

        activate(12)
        set_stack([
            ("Other • finished", "Return → Hello: line 8", done),
            hello_waiting, startup_waiting,
        ])
        set_state("Other finishes", done)
        self.wait(1.2)
        complete(12)

        set_state("Return 1: Other → Hello", saved)
        transfer(12, 6, saved)
        activate(6)
        set_stack([
            ("Hello • resumed", "At Other(): line 8", active), startup_waiting,
        ])
        self.wait(0.8)
        complete(6)
        set_stack([
            ("Hello • resumed", "Other() completed", done), startup_waiting,
        ])
        set_state("Other(); completed • back in Hello", done)
        self.wait(1.2)

        activate(7)
        set_stack([
            ("Hello • finished", "Return → startup: line 1", done), startup_waiting,
        ])
        set_state("Hello finishes after its final call", done)
        self.wait(1.6)
        complete(7)

        set_state("Return 2: Hello → startup", saved)
        transfer(7, 0, saved)
        activate(0)
        set_stack([("Startup • resumed", "At Hello(): line 1", active)])
        self.wait(0.8)
        complete(0)
        set_stack([("Startup • resumed", "Hello() completed", done)])
        set_state("Hello(); completed • back in startup", done)
        self.wait(1.2)

        set_state("No startup statements remain", done)
        self.wait(1.2)
        set_stack([])
        self.add(at_left("Program ended", 2.10, 1.58, 22, done))
        set_state("Program ends", done)
        self.wait(2)
