import React from 'react';

import Navbar from "./common/Navbar";

export class Layout extends React.Component {
  render () {
    return (
      <div>
          <Navbar/>
          {this.props.children}
      </div>
    );
  }
}
