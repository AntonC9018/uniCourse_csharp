"""Make three requested delays visible without implying exact scheduling.

Reduced reconstruction of the seven-statement sample at video t=280–309 s.
The timer is illustrative; only its 0-to-1000 sweep takes one video second.
Reading holds around the sweep are teaching time, not simulated execution time.
"""

from manim import (
    Scene, Text, VGroup, RoundedRectangle, Line, Polygon,
    ValueTracker, TransformFromCopy, LEFT, RIGHT, linear,
)


class CourseAnimation(Scene):
    """Follow each print, each completed Sleep, and each continuation."""

    def construct(self):
        self.camera.background_color = "#101827"
        ink = "#EEF3FC"
        muted = "#AABBD2"
        accent = "#F6C85F"
        done = "#75DEB3"
        border = "#40536E"
        mono = "DejaVu Sans Mono"
        sans = "DejaVu Sans"

        def label(text, size=22, color=ink, font=sans):
            return Text(text, font=font, font_size=size, color=color)

        title = label("Thread.Sleep(1000)", 34)
        title.move_to([0, 3.38, 0])
        subtitle = label("Requested pause: 1000 ms = 1 s", 23, muted)
        subtitle.move_to([0, 2.79, 0])

        source_panel = RoundedRectangle(
            width=8.2, height=4.25, corner_radius=0.16,
            stroke_color=border, stroke_width=1.5,
            fill_color="#182438", fill_opacity=1,
        ).move_to([-2.5, 0.19, 0])
        console_panel = RoundedRectangle(
            width=3.8, height=4.25, corner_radius=0.16,
            stroke_color=border, stroke_width=1.5,
            fill_color="#111E2D", fill_opacity=1,
        ).move_to([4.5, 0.19, 0])
        source_heading = label("Program.cs", 24)
        source_heading.move_to([-6.22, 1.93, 0], aligned_edge=LEFT)
        console_heading = label("Console output", 24)
        console_heading.move_to([2.94, 1.93, 0], aligned_edge=LEFT)
        rules = VGroup(
            Line([-6.22, 1.57, 0], [1.22, 1.57, 0], color=border),
            Line([2.94, 1.57, 0], [6.06, 1.57, 0], color=border),
        )

        statements = [
            'Console.WriteLine("Hello world!");',
            "Thread.Sleep(1000);",
            "Console.WriteLine(1);",
            "Thread.Sleep(1000);",
            "Console.WriteLine(2);",
            "Thread.Sleep(1000);",
            "Console.WriteLine(3);",
        ]
        row_y = [1.15 - 0.43 * index for index in range(7)]
        code_rows = VGroup()
        numbers = VGroup()
        for index, (statement, y) in enumerate(zip(statements, row_y)):
            code = label(statement, 22, font=mono)
            code.move_to([-5.76, y, 0], aligned_edge=LEFT)
            code_rows.add(code)
            number = label(str(index + 1), 20, muted, mono)
            number.move_to([-6.08, y, 0])
            numbers.add(number)

        outputs = []
        for index, value in enumerate(["Hello world!", "1", "2", "3"]):
            output = label(value, 25, font=mono)
            output.move_to([2.96, 1.15 - 0.65 * index, 0], aligned_edge=LEFT)
            outputs.append(output)
        empty = label("No output yet", 22, muted)
        empty.move_to([2.96, 1.15, 0], aligned_edge=LEFT)

        # A gutter pointer and text color identify the active statement without
        # covering the source. It remains stationary throughout each timer sweep.
        pointer = Polygon(
            [-6.47, 0.10, 0], [-6.31, 0, 0], [-6.47, -0.10, 0],
            stroke_width=0, fill_color=accent, fill_opacity=1,
        )
        status = label("Ready to run", 26)
        status.move_to([0, -2.25, 0])
        detail = label(" ", 22, muted)
        detail.move_to([0, -2.78, 0])

        timer_title = label("Illustrative requested delay", 22, accent)
        timer_title.move_to([-1.05, -2.78, 0], aligned_edge=RIGHT)
        timer_value = label("0", 22, accent, mono)
        timer_value.move_to([0.65, -2.78, 0], aligned_edge=RIGHT)
        timer_unit = label("/ 1000 ms", 22, accent, mono)
        timer_unit.move_to([0.85, -2.78, 0], aligned_edge=LEFT)
        timer = ValueTracker(0)
        track_left = -3.0
        track_width = 6.0
        track = Line(
            [track_left, -3.23, 0], [track_left + track_width, -3.23, 0],
            color=border, stroke_width=8,
        )
        progress = Line(
            [track_left, -3.23, 0], [track_left + 0.001, -3.23, 0],
            color=accent, stroke_width=8,
        )
        start_label = label("0 ms", 18, muted, mono).move_to([-3.65, -3.23, 0])
        end_label = label("1000 ms", 18, muted, mono).move_to([3.9, -3.23, 0])
        timer_objects = VGroup(
            timer_title, timer_value, timer_unit, track, progress,
            start_label, end_label,
        )
        footer = label(
            "Each sweep: 1 s • Reading holds added • Actual resumption may be later.", 18, muted,
        )
        footer.move_to([0, -3.69, 0])

        def set_state(primary, secondary=None, color=ink):
            nonlocal status, detail
            self.remove(status, detail)
            status = label(primary, 26, color)
            status.move_to([0, -2.25, 0])
            self.add(status)
            if secondary is not None:
                detail = label(secondary, 22, muted)
                detail.move_to([0, -2.78, 0])
                self.add(detail)

        def activate(index):
            pointer.move_to([-6.39, row_y[index], 0])
            self.add(pointer)
            code_rows[index].set_color(accent)
            numbers[index].set_color(accent)

        def complete(index):
            code_rows[index].set_color(done)
            numbers[index].set_color(done)

        def append_output(index):
            if index == 0:
                self.remove(empty)
            # Copy the literal's displayed value from its fixed code position.
            # Full code rows retain their original character spacing throughout.
            line_index = 0 if index == 0 else 2 * index
            value = ["Hello world!", "1", "2", "3"][index]
            statement = statements[line_index]
            literal_start = statement.index(value)
            literal_center = literal_start + len(value) / 2
            row = code_rows[line_index]
            source_value = label(value, 22, accent, mono)
            source_value.move_to([
                row.get_left()[0] + row.width * literal_center / len(statement),
                row_y[line_index], 0,
            ])
            self.play(
                TransformFromCopy(source_value, outputs[index], path_arc=-0.18),
                run_time=0.8,
            )
            self.wait(0.7)

        self.add(
            title, subtitle, source_panel, console_panel, source_heading,
            console_heading, rules, code_rows, numbers, empty, status, detail, footer,
        )
        self.wait(2.0)

        activate(0)
        set_state(
            "Print",
            " ",
            accent,
        )
        self.wait(0.8)
        append_output(0)
        complete(0)

        # Timeline per cycle: 0.4 s at zero, 1.0 s sweep, 0.8 s completed,
        # 0.9 s continuation, 0.8 s value transfer, 0.7 s output hold.
        for pause_index in range(3):
            sleep_line = 1 + 2 * pause_index
            print_line = sleep_line + 1
            activate(sleep_line)
            set_state(
                "Sleep • Console unchanged",
                color=accent,
            )
            timer.set_value(0)
            timer_value.become(label("0", 22, accent, mono))
            timer_value.move_to([0.65, -2.78, 0], aligned_edge=RIGHT)
            progress.put_start_and_end_on(
                [track_left, -3.23, 0], [track_left + 0.001, -3.23, 0],
            )
            self.add(timer_objects)

            timer_value.add_updater(
                lambda number: number.become(
                    label(str(round(timer.get_value())), 22, accent, mono)
                ).move_to([0.65, -2.78, 0], aligned_edge=RIGHT)
            )
            progress.add_updater(
                lambda bar: bar.put_start_and_end_on(
                    [track_left, -3.23, 0],
                    [
                        track_left + max(0.001, track_width * timer.get_value() / 1000),
                        -3.23, 0,
                    ],
                )
            )
            self.wait(0.4)
            self.play(timer.animate.set_value(1000), run_time=1.0, rate_func=linear)
            timer_value.clear_updaters()
            progress.clear_updaters()
            # Pin both visual endpoints after interpolation, before continuation.
            progress.put_start_and_end_on(
                [track_left, -3.23, 0],
                [track_left + track_width, -3.23, 0],
            )
            timer_value.become(label("1000", 22, accent, mono))
            timer_value.move_to([0.65, -2.78, 0], aligned_edge=RIGHT)
            complete(sleep_line)
            set_state(
                "Sleep completed",
                color=done,
            )
            self.wait(0.8)

            self.remove(timer_objects)
            activate(print_line)
            set_state(
                "Continue → Print",
                " ",
                accent,
            )
            self.wait(0.9)
            append_output(pause_index + 1)
            complete(print_line)

        self.remove(pointer)
        set_state(
            "Program complete",
            " ",
            done,
        )
        self.wait(3.1)
