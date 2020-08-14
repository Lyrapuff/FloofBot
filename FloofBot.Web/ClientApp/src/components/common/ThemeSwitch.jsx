import React from 'react';

import { faSun, faMoon } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

export default class ThemeSwitch extends React.Component {
    constructor() {
        super();

        this.state = {
            theme: 'dark'
        };
    }
    
    componentDidMount = () =>  {
        let theme = localStorage.getItem('theme') || 'dark';

        this.setState({
            theme
        });
        
        document.documentElement.setAttribute('theme', theme);
    }

    switchTheme = () => {
        let theme = localStorage.getItem('theme') || 'dark';
        theme = theme === 'dark' ? 'light' : 'dark';

        this.setState({
            theme
        });
        
        localStorage.setItem('theme', theme);

        document.documentElement.setAttribute('theme', theme);
    }
    
    render() {
        return (
            <div className={this.props.className} onClick={this.switchTheme}>
                <div className="ButtonSecondary">
                    <FontAwesomeIcon style={{fontSize: '22px', verticalAlign: 'middle'}} icon={this.state.theme === 'dark' ? faSun : faMoon}/>
                </div>
            </div>
        );
    }
}