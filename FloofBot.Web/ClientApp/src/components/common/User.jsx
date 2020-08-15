import React from 'react';

import {Button} from "antd";

export default class User extends React.Component {
    authUrl = '/login';
    
    render() {
        return (
            <div>
                <a href={this.authUrl}>
                    <Button type="primary">
                        Login
                    </Button>
                </a>
            </div>
        );
    }
}