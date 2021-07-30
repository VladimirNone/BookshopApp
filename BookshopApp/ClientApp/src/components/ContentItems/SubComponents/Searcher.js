import React, { Component } from 'react';

export class Searcher extends Component {

    render() {
        return (
            <div className="input-group mb-3">
                <input type="text" className="form-control" placeholder="Название продукта" />
                <div className="input-group-append">
                    <button className="btn btn-outline-secondary" type="button">Поиск</button>
                </div>
            </div>);
    }
}