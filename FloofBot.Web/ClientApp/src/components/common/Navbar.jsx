import React from 'react';

import {Link} from "react-router-dom";
import User from "./User";

import './navbar.scss';

export default class Navbar extends React.Component {
    render() {
        return (
            <div className="Navbar">
                <div className="NavItem NormalFont">
                    <Link className="BlandLink" to="/">FloofyBot</Link>
                </div>
                <div className="NavItem"/>
                <div className="NavItem SmallFont">
                    <Link className="BlandLink" to="/dashboard">Dashboard</Link>
                </div>
                <div className="NavItem SmallFont">
                    Commands
                </div>
                <div className="NavItem SmallFont">
                    Support Server
                </div>
                
                <div style={{flexGrow: 1}}/>
                
                <div className="NavItem">
                    <User/>
                </div>
            </div>
        );
    }
}