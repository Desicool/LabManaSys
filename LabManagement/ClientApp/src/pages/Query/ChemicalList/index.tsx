import React, { FC, useRef, useState, useEffect } from 'react';
import {
  Card,
  Input,
  List,
  Radio,
} from 'antd';

import { findDOMNode } from 'react-dom';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect, Dispatch, UserModelState, IRole } from 'umi';
import { ChemicalListModelState } from './model';
import styles from './style.less';
import { IChemical } from '@/models/entity';

const RadioButton = Radio.Button;
const RadioGroup = Radio.Group;
const { Search } = Input;

interface ChemicalListProps {
  chemicalListState: ChemicalListModelState;
  dispatch: Dispatch;
  loading: boolean;
  roles: IRole[];
}
/*
const Info: FC<{
  title: React.ReactNode;
  value: React.ReactNode;
  bordered?: boolean;
}> = ({ title, value, bordered }) => (
  <div className={styles.headerInfo}>
    <span>{title}</span>
    <p>{value}</p>
    {bordered && <em />}
  </div>
);
*/
const ListContent = ({
  data: { state },
}: {
  data: IChemical;
}) => (
    <div className={styles.listContent}>
      <div className={styles.listContentItem}>
        <span>在库状态</span>
        <p>{state}</p>
      </div>
    </div>
  );

export const ChemicalList: FC<ChemicalListProps> = (props) => {
  const {
    loading,
    dispatch,
    chemicalListState: { list },
    roles
  } = props;

  useEffect(() => {
    dispatch({
      type: 'chemicalList/fetch',
    });
  }, [1]);

  const paginationProps = {
    showSizeChanger: true,
    showQuickJumper: true,
    pageSize: 5,
    total: list.length,
  };

  const deleteItem = (chemical: IChemical) => {
    dispatch({
      type: 'chemicalList/destroy',
      payload: chemical,
    });
  };

  const extraContent = (
    <div className={styles.extraContent}>
      <RadioGroup defaultValue="all">
        <RadioButton value="all">全部</RadioButton>
        <RadioButton value="progress">空闲</RadioButton>
        <RadioButton value="waiting">使用中</RadioButton>
      </RadioGroup>
      <Search className={styles.extraContentSearch} placeholder="请输入" onSearch={() => ({})} />
    </div>
  );
  return (
    <div>
      <PageHeaderWrapper>
        <div className={styles.standardList}>

          <Card
            className={styles.listCard}
            bordered={false}
            title="基本列表"
            style={{ marginTop: 24 }}
            bodyStyle={{ padding: '0 32px 40px 32px' }}
            extra={extraContent}
          >
            <List
              size="large"
              rowKey="id"
              loading={loading}
              pagination={paginationProps}
              dataSource={list}
              renderItem={(item) => (
                <List.Item
                  actions={
                    roles.filter(r => r.roleName === 'LabTeacher').length > 0 ? [
                      <a
                        key="discard"
                        onClick={(e) => {
                          e.preventDefault();
                          deleteItem(item);
                        }}
                      >
                        销毁
                    </a>,
                    ] : []}
                >
                  <List.Item.Meta
                    title={<span>{item.name}</span>}
                    description={<div>
                      <span>{item.amount}</span>
                      <span>{item.unitmeasurement}</span>
                    </div>}
                  />
                  <ListContent data={item} />
                </List.Item>
              )}
            />
          </Card>
        </div>
      </PageHeaderWrapper>
    </div>
  );
};

export default connect(
  ({
    chemicalList,
    loading,
    user
  }: {
    chemicalList: ChemicalListModelState;
    user: UserModelState;
    loading: {
      models: { [key: string]: boolean };
    };
  }) => ({
    chemicalListState: chemicalList,
    loading: loading.models.chemicalList,
    roles: user.roles || [],
  }),
)(ChemicalList);
