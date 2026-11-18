"""Trace one call and its saved continuation (recording t=346–439 s).

The complete C# sample is the note's reduced reconstruction, not an exact
recorded editor state. Reading holds illustrate control flow, not CPU time.
"""

from manim import (
    Scene, Text, VGroup, RoundedRectangle, Line, Polygon, Dot, VMobject,
    FadeIn, MoveAlongPath, LEFT, linear,
)


class CourseAnimation(Scene):
    """Run startup, enter all three body statements, then resume this caller."""

    def construct(self):
        self.camera.background_color = "#101827"
        ink = "#EEF3FC"
        muted = "#AABBD2"
        active_color = "#F6C85F"
        done = "#75DEB3"
        saved_color = "#83C9F4"
        border = "#40536E"
        mono = "DejaVu Sans Mono"
        sans = "DejaVu Sans"

        def label(text, size=22, color=ink, font=sans):
            return Text(text, font=font, font_size=size, color=color)

        def left_label(text, x, y, size=22, color=ink, font=sans):
            return label(text, size, color, font).move_to(
                [x, y, 0], aligned_edge=LEFT,
            )

        title = label("Example()", 32)
        title.move_to([0, 3.45, 0])
        subtitle = label("Startup → body → continuation", 23, muted)
        subtitle.move_to([0, 2.91, 0])
        source_panel = RoundedRectangle(
            width=8.2, height=5.0, corner_radius=0.14,
            stroke_color=border, stroke_width=1.5,
            fill_color="#182438", fill_opacity=1,
        ).move_to([-2.5, 0.10, 0])
        console_panel = RoundedRectangle(
            width=3.8, height=5.0, corner_radius=0.14,
            stroke_color=border, stroke_width=1.5,
            fill_color="#111E2D", fill_opacity=1,
        ).move_to([4.5, 0.10, 0])
        headings = VGroup(
            left_label("Program.cs • Startup / caller", -6.18, 2.24, 23),
            left_label("Console output", 2.94, 2.24, 23),
            left_label("Definition • Example body", -6.18, 0.02, 20, muted),
        )
        rules = VGroup(
            Line([-6.18, 1.99, 0], [1.22, 1.99, 0], color=border),
            Line([2.94, 1.99, 0], [6.06, 1.99, 0], color=border),
            Line([-6.18, 0.25, 0], [1.22, 0.25, 0], color=border),
            Line([2.94, -0.87, 0], [6.06, -0.87, 0], color=border),
        )

        # Blank source line 5 separates startup from the function definition.
        statements = [
            'Console.WriteLine("Hello world!");',
            'Console.WriteLine("Anton");',
            "Example();",
            'Console.WriteLine("Anton");',
            "void Example()",
            "{",
            "    Console.WriteLine(1);",
            "    Console.WriteLine(2);",
            "    Console.WriteLine(3);",
            "}",
        ]
        source_numbers = [1, 2, 3, 4, 6, 7, 8, 9, 10, 11]
        row_y = [1.71, 1.29, 0.87, 0.45, -0.42, -0.74,
                 -1.06, -1.38, -1.70, -2.02]
        pitch = (label("00000000000", 21, ink, mono).width
                 - label("0000000000", 21, ink, mono).width)
        code_rows = VGroup()
        numbers = VGroup()
        for statement, number, y in zip(statements, source_numbers, row_y):
            code_x = -5.79 + (4 * pitch if 8 <= number <= 10 else 0)
            code_rows.add(left_label(statement.lstrip(), code_x, y, 21, ink, mono))
            numbers.add(
                label(str(number), 18, muted, mono).move_to([-6.10, y, 0])
            )

        output_rows = [
            left_label(value, 2.98, 1.66 - 0.42 * index, 24, ink, mono)
            for index, value in enumerate(
                ["Hello world!", "Anton", "1", "2", "3", "Anton"]
            )
        ]
        empty = left_label("No output yet", 2.98, 1.66, 21, muted)
        call_state = left_label("No call yet", 2.98, -1.16, 20, muted)
        saved_heading = left_label("Saved continuation", 2.98, -1.53, 18, muted)
        saved_line = left_label("—", 2.98, -1.90, 20, muted)
        saved_marker = Polygon(
            [-6.42, 0.45, 0], [-6.34, 0.53, 0],
            [-6.26, 0.45, 0], [-6.34, 0.37, 0],
            stroke_color=saved_color, stroke_width=2,
            fill_opacity=0,
        )
        waiting_label = left_label("waiting", -0.10, 0.45, 18, saved_color)
        pointer = Polygon(
            [-6.48, 0.10, 0], [-6.30, 0, 0], [-6.48, -0.10, 0],
            stroke_width=0, fill_color=active_color, fill_opacity=1,
        )

        phase = label("Startup", 26).move_to([0, -2.90, 0])

        def set_phase(text, color=ink):
            nonlocal phase
            self.remove(phase)
            phase = label(text, 26, color).move_to([0, -2.90, 0])
            self.add(phase)

        def set_call(primary, continuation, color):
            nonlocal call_state, saved_line
            self.remove(call_state, saved_line)
            call_state = left_label(primary, 2.98, -1.16, 20, color)
            saved_line = left_label(continuation, 2.98, -1.90, 20, color)
            self.add(call_state, saved_line)

        def activate(index):
            pointer.move_to([-6.39, row_y[index], 0])
            self.add(pointer)
            code_rows[index].set_color(active_color)
            numbers[index].set_color(active_color)

        def complete(index):
            code_rows[index].set_color(done)
            numbers[index].set_color(done)
            self.remove(pointer)

        def print_value(source_index, output_index):
            activate(source_index)
            self.wait(0.45)
            if output_index == 0:
                self.remove(empty)
            # Copy the literal's displayed value while leaving the source intact.
            # A complete monospaced line retains its character advances.
            value = ["Hello world!", "Anton", "1", "2", "3", "Anton"][output_index]
            is_string = output_index in (0, 1, 5)
            prefix = 'Console.WriteLine("' if is_string else "Console.WriteLine("
            indent = 4 if source_index >= 6 else 0
            start_x = -5.79 + (indent + len(prefix)) * pitch
            travelling = left_label(
                value, start_x, row_y[source_index], 21, active_color, mono,
            )
            self.add(travelling)
            destination = output_rows[output_index]
            # Stay on the active statement's baseline until the source ends,
            # then use the gap between code and console for vertical motion.
            path = VMobject().set_points_as_corners([
                travelling.get_center(),
                [2.05, row_y[source_index], 0],
                [2.05, destination.get_y(), 0],
                destination.get_center(),
            ])
            self.play(
                MoveAlongPath(travelling, path),
                run_time=1.1, rate_func=linear,
            )
            self.remove(travelling)
            self.add(destination)
            self.wait(0.55)
            complete(source_index)
            self.wait(0.30)

        def transfer(start_y, end_y):
            # Route outside the source: crossing a line does not execute it.
            path = VMobject().set_points_as_corners([
                [-6.38, start_y, 0], [-6.86, start_y, 0],
                [-6.86, end_y, 0], [-6.38, end_y, 0],
            ])
            path.set_stroke(active_color, width=2)
            token = Dot(path.get_start(), radius=0.055, color=active_color)
            self.play(FadeIn(path), FadeIn(token), run_time=0.25)
            self.play(
                MoveAlongPath(token, path), run_time=1.2, rate_func=linear,
            )
            self.remove(path, token)

        self.add(
            title, subtitle, source_panel, console_panel, headings, rules,
            code_rows, numbers, empty, call_state, saved_heading, saved_line,
            phase,
        )
        self.wait(1.5)
        print_value(0, 0)
        print_value(1, 1)

        activate(2)
        set_call("Call in progress", "Startup → line 4", saved_color)
        self.add(saved_marker, waiting_label)
        set_phase("Call", active_color)
        self.wait(2.0)

        self.remove(pointer)
        transfer(row_y[2], row_y[6])
        set_phase("Example body", active_color)
        activate(6)
        self.wait(0.6)
        for body_index in range(3):
            print_value(6 + body_index, 2 + body_index)

        set_phase("Body complete", done)
        self.wait(1.0)
        set_phase("Return", saved_color)
        transfer(row_y[8], row_y[2])
        complete(2)
        set_call("Call completed", "Startup → line 4", done)
        set_phase("Call complete", done)
        self.wait(1.2)

        self.remove(saved_marker, waiting_label)
        set_call("Call completed", "Resume line 4", done)
        set_phase("Startup", active_color)
        print_value(3, 5)
        set_call("Call completed", "Continuation used", done)
        set_phase("Startup complete", done)
        self.wait(2.0)
