import React, { Component, Fragment } from 'react';
import { Searcher } from './Searcher';
import { Card } from './Card';

export class MainPage extends Component {

  render () {
      return (
          <Fragment>
              <Searcher />
              <Card />
          </Fragment>
      );
  }
}
