use std::fs;

fn main(){
    let filename = "example2.txt";
    match fs::read_to_string(filename)
    {
        Ok(contents)=>{
            let lines = contents.lines();
            let mut total = 0;
            

            for (_, line) in lines.enumerate() {
                let mut first_digit : Option<u32> = None;
                let mut last_digit: Option<u32> = None;

                let mut line_chars_enum = line.chars();
                let mut character = line_chars_enum.next();
                if character == None {
                    break;
                }

                loop
                {
                   let mut digit: Option<u32> = None;

                    loop{
                        if character.unwrap().is_digit(10) {
                            let character = character.unwrap();
                            print!("{} ",character);
                            digit = character.to_digit(10);
                        }

                        match character {
                            Some('o')=>{
                                character = line_chars_enum.next();
                                if character == Some('n') {
                                    character = line_chars_enum.next();
                                    if character == Some('e') {
                                        digit = Some(1);
                                        print!("one ");
                                    }
                                }
                            },
                            Some('t') =>{
                                character = line_chars_enum.next();
                                if character == Some('w') {
                                    character = line_chars_enum.next();
                                    if character == Some('o') {
                                        digit = Some(2);
                                        print!("two ");
                                    }
                                }
                                else if character == Some('h') {
                                    character = line_chars_enum.next();
                                    if character == Some('r') {
                                        character = line_chars_enum.next();
                                        if character == Some('e') {
                                            character = line_chars_enum.next();
                                            if character == Some('e') {
                                                digit = Some(3);
                                                print!("three ");
                                            }
                                        }
                                    }
                                }
                            },
                            //Some('f') =>{
                            //    character = line_chars_enum.next();
                            //    if character == Some('o') {
                            //        character = line_chars_enum.next();
                            //        if character == Some('u') {
                            //            character = line_chars_enum.next();
                            //            if character == Some('r') {
                            //                digit = Some(4);
                            //                print!("four ");
                            //            }
                            //        }
                            //    }
                            //    else if character == Some('i') {
                            //        character = line_chars_enum.next();
                            //        if character == Some('v') {
                            //            character = line_chars_enum.next();
                            //            if character == Some('e') {
                            //                digit = Some(5);
                            //                print!("five ");
                            //            }
                            //        }
                            //    }
                            //},
                            //Some('s') =>{
                            //    character = line_chars_enum.next();
                            //    if character == Some('i') {
                            //        character = line_chars_enum.next();
                            //        if character == Some('x') {
                            //            digit = Some(6);
                            //            print!("six ");
                            //        }
                            //    }
                            //    else if character == Some('e') {
                            //        character = line_chars_enum.next();
                            //        if character == Some('v') {
                            //            character = line_chars_enum.next();
                            //            if character == Some('e') {
                            //                character = line_chars_enum.next();
                            //                if character == Some('n') {
                            //                    digit = Some(7);
                            //                    print!("seven ");
                            //                }
                            //            }
                            //        }
                            //    }
                            //},
                            //Some('e')=>{
                            //    character = line_chars_enum.next();
                            //    if character == Some('i') {
                            //        character = line_chars_enum.next();
                            //        if character == Some('g') {
                            //            character = line_chars_enum.next();
                            //            if character == Some('h') {
                            //                character = line_chars_enum.next();
                            //                if character == Some('t') {
                            //                    digit = Some(8);
                            //                    print!("eight ");
                            //                }
                            //            }
                            //        }
                            //    }
                            //},
                            //Some('n') =>{
                            //    if character == Some('i') {
                            //        character = line_chars_enum.next();
                            //        if character == Some('n') {
                            //            character = line_chars_enum.next();
                            //            if character == Some('e') {
                            //                digit = Some(9);
                            //                print!("nine ");
                            //            }
                            //        }
                            //    }
                            //},
                            _ => { break; }
                        }

                        if let Some(_) = digit {
                            if first_digit == None {
                                first_digit = digit;
                            }
                            last_digit = digit;
                        }
                    }

                    character = line_chars_enum.next();
                    if character == None {
                        break;
                    }

                }
                
                println!(" : {}", line);

                if let Some(digit) = first_digit {
                    total += 10 * digit;
                }
                if let Some(digit) = last_digit {
                    total += digit;
                }
            }
            
            println!("Total: {}", total);
        },
        Err(error)=>{
            println!("Failed to read file: {}", error);
        }
    }
}