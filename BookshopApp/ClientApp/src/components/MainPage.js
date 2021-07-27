import React, { Component } from 'react';

export class MainPage extends Component {
  static displayName = MainPage.name;

  render () {
      return (
		  <div className="row">
			  <div className="col-sm border">
				  Один из трех столбцов
				</div>
			  <div className="col-sm-7 border">
				  <div className="row border">Поиск</div>
				  <div className="row border">
					  <img width="100%" src="/Images/no_foto.png" alt="image" />
				  </div>
				  <div className="row border">Второй продукт</div>
				</div>
			  <div className="col-sm border">
				  <div className="row border">one</div>
				  <div className="row border">two</div>
				</div>
          </div>
    );
  }
}
