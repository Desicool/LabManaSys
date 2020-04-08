import React, { FC, useRef, useState, useEffect } from 'react';
import {
  Card,
  Input,
  List,
  Radio,
} from 'antd';

import { findDOMNode } from 'react-dom';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { connect, Dispatch } from 'umi';
import OperationModal from './components/OperationModal';
import { StateType } from './model';
import styles from './style.less';
import { IChemical } from '@/models/entity';

const RadioButton = Radio.Button;
const RadioGroup = Radio.Group;
const { Search } = Input;

interface ChemicalListProps {
  chemicalListState: StateType;
  dispatch: Dispatch;
  loading: boolean;
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
  const addBtn = useRef(null);
  const {
    loading,
    dispatch,
    chemicalListState: { list },
  } = props;
  const [done, setDone] = useState<boolean>(false);
  const [visible, setVisible] = useState<boolean>(false);
  const [current, setCurrent] = useState<Partial<IChemical> | undefined>(undefined);

  useEffect(() => {
    dispatch({
      type: 'chemicalList/fetch',
      payload: {
        count: 5,
      },
    });
  }, [1]);

  const paginationProps = {
    showSizeChanger: true,
    showQuickJumper: true,
    pageSize: 5,
    total: 50,
  };

  const showModal = () => {
    setVisible(true);
    setCurrent(undefined);
  };

  const showEditModal = (item: IChemical) => {
    setVisible(true);
    setCurrent(item);
  };

  const deleteItem = (id: number) => {
    dispatch({
      type: 'chemicalList/submit',
      payload: { id },
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

  const setAddBtnblur = () => {
    if (addBtn.current) {
      // eslint-disable-next-line react/no-find-dom-node
      const addBtnDom = findDOMNode(addBtn.current) as HTMLButtonElement;
      setTimeout(() => addBtnDom.blur(), 0);
    }
  };

  const handleDone = () => {
    setAddBtnblur();

    setDone(false);
    setVisible(false);
  };

  const handleCancel = () => {
    setAddBtnblur();
    setVisible(false);
  };

  const handleSubmit = (values: IChemical) => {
    const id = current ? current.id : '';

    setAddBtnblur();

    setDone(true);
    dispatch({
      type: 'chemicalList/submit',
      payload: { id, ...values },
    });
  };

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
                  actions={[
                    <a
                      key="edit"
                      onClick={(e) => {
                        e.preventDefault();
                        showEditModal(item);
                      }}
                    >
                      编辑
                    </a>,
                    <a
                      key="discard"
                      onClick={(e) => {
                        e.preventDefault();
                        deleteItem(item.id as number);
                      }}
                    >
                      销毁
                    </a>,
                  ]}
                >
                  <List.Item.Meta
                    title={<span>{item.name}</span>}
                    description={<div>
                      <span>{(item.unitprice as number) * (item.amount as number)}</span>
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

      <OperationModal
        done={done}
        current={current}
        visible={visible}
        onDone={handleDone}
        onCancel={handleCancel}
        onSubmit={handleSubmit}
      />
    </div>
  );
};

export default connect(
  ({
    chemicalList,
    loading,
  }: {
    chemicalList: StateType;
    loading: {
      models: { [key: string]: boolean };
    };
  }) => ({
    chemicalListState: chemicalList,
    loading: loading.models.chemicalList,
  }),
)(ChemicalList);
