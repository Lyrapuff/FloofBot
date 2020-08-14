import React from 'react';

import ThemeSwitch from "./ThemeSwitch";
import {Link} from "react-router-dom";

import './navbar.scss';

export default class Navbar extends React.Component {
    render() {
        return (
            <div className="Navbar">
                <div className="NavItem NormalFont">
                    <Link to="/">FloofyBot</Link>
                </div>
                <div className="NavItem"/>
                <div className="NavItem SmallFont">
                    <Link to="/dashboard">Dashboard</Link>
                </div>
                <div className="NavItem SmallFont">
                    Commands
                </div>
                <div className="NavItem SmallFont">
                    Support Server
                </div>
                
                <div style={{flexGrow: 1}}/>
                
                <div className="NavItem">
                    <div className="ButtonPrimary">
                        Login with Discord
                    </div>
                </div>

                <ThemeSwitch className="NavItem"/>
            </div>
        );
    }
}