import React from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './pages/Home';
import Dashboard from './pages/Dashboard';

import "./main.scss";

export default class App extends React.Component {
  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route exact path='/dashboard' component={Dashboard} />
      </Layout>
    );
  }
}
